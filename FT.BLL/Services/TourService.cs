using FT.BLL.DTOs.TourDtos;
using FT.BLL.Mapper.Tour;
using FT.BLL.Validators.Tour;
using FT.Domain.Entities;
using FT.Domain.Utilities;
using FT.Infrastructure.Interfaces;
using FT.Utilities.Request;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static FT.Utils.Enums;

namespace FT.BLL.Services
{
    public class TourService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CreateTourDtoValidator _validatorCreate;
        private readonly UpdateTourDtoValidator _validatorUpdate;

        public TourService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _validatorCreate = new CreateTourDtoValidator();
            _validatorUpdate = new UpdateTourDtoValidator();

        }

        public async Task<Response> AddTour(CreateTourDto dto)
        {
            if (dto == null)
                return Response.Error("Los datos del tour son requeridos.");

            var validationResult = _validatorCreate.Validate(dto);
            if (!validationResult.IsValid)
            {
                var errores = validationResult.Errors.Select(e => new { campo = e.PropertyName, mensaje = e.ErrorMessage }).ToList();
                return new Response(400, "ERROR", "Error de validación.", errores);
            }

            // 🔹 Validar que no haya otro tour con el mismo nombre
            bool existeNombre = await _unitOfWork.Tours.AnyAsync(t => t.Name.ToLower() == dto.Name.ToLower());
            if (existeNombre)
                return Response.Error($"Ya existe un tour con el nombre '{dto.Name}'.", 409);

            var tour = dto.ToEntity();
            await _unitOfWork.Tours.AddAsync(tour);
            await _unitOfWork.SaveChangesAsync();

            return Response.Success(tour.ToReadDto(), "Tour creado correctamente");
        }

        public async Task<Response> TourList(ToursFilterRequest? filters = null)
        {
            filters ??= new ToursFilterRequest();

            State? stateFilter = null;

            if (filters.StateFilter.HasValue &&
                Enum.IsDefined(typeof(State), filters.StateFilter.Value))
            {
                stateFilter = (State)filters.StateFilter.Value;
            }

            // Filtro aplicado correctamente
            Expression<Func<Tour, bool>> filtroExtra = t =>
                // Filtro: texto
                (string.IsNullOrEmpty(filters.TextFilter) ||
                 EF.Functions.Like(t.Name ?? "", $"%{filters.TextFilter}%"))

                &&

                // Filtro: estado
                (!stateFilter.HasValue || t.State == stateFilter.Value);

            var response = await _unitOfWork.Tours.ListAsync(filters, filtroExtra);

            // Acceso correcto a los datos dentro del Response
            var data = response.Data;  // <- adentro está TotalRecords, Records, etc.

            //Console.WriteLine($"Tours encontrados: {data.TotalRecords}");

            return response;
        }


        // ----------------- Modificar tour -----------------
        public async Task<Response> UpdateTourAsync(UpdateTourDto dto)
        {
            try
            {
                if (dto == null)
                    return Response.Error("Los datos del tour son requeridos.", 400);

                // Validación del DTO
                var validation = _validatorUpdate.Validate(dto);

                if (!validation.IsValid)
                {
                    var errores = validation.Errors.Select(e => e.ErrorMessage).ToList();
                    return Response.ValidationError(errores);
                }

                // Buscar entidad
                var tour = await _unitOfWork.Tours.GetByIdAsync(dto.Id);
                if (tour == null)
                    return Response.Error("El tour no existe.", 404);

                // Actualizar mediante método de extensión
                tour.UpdateFromDto(dto);

                _unitOfWork.Tours.Update(tour);
                await _unitOfWork.SaveChangesAsync();

                var dtoActualizado = tour.ToReadDto();

                return Response.Success(dtoActualizado, "Tour actualizado correctamente");
            }
            catch (Exception ex)
            {
                return Response.Error("Error inesperado: " + ex.Message, 500);
            }
        }


        // ----------------- Eliminar tour -----------------
        public async Task<Response> RemoveTourAsync(int idTour)
        {
            var tour = await _unitOfWork.Tours.GetByIdAsync(idTour);

            if (tour == null)
                return Response.Error("El tour no existe", 404);

            _unitOfWork.Tours.Remove(tour);
            await _unitOfWork.SaveChangesAsync();


            return Response.Success(null, "Tour eliminado correctamente");

        }


        // ----------------- Obtener tour por Id -----------------
        public async Task<Response> GetTourByIdAsync(int idTour)
        {
            var tour = await _unitOfWork.Tours.GetByIdAsync(idTour);

            if (tour == null)
                return Response.Error("El tour no existe", 404);

            return Response.Success(tour.ToReadDto(), "Tour encontrado");

        }
    }
}
