using FT.BLL.DTOs.Tour;
using FT.BLL.DTOs.TourDtos;
using static FT.Utils.Enums;


namespace FT.BLL.Mapper.Tour
{
    public static class TourMapper
    {
        // 🟢 De DTO a Entidad (para crear)
        public static Domain.Entities.Tour ToEntity(this CreateTourDto dto)
        {
            return new FT.Domain.Entities.Tour
            {
                Name = dto.Name ?? string.Empty,
                Price = dto.Price ?? 0,
                Description = dto.Description ?? string.Empty,
                Duration = DurationDays.FullDay, // valor por defecto
                Created = DateTime.Now
            };
        }

        // 🟢 De Entidad a DTO de lectura (si luego creas un ReadTourDto)
        public static ReadTourDto ToReadDto(this Domain.Entities.Tour dto)
        {
            return new ReadTourDto
            {
                Id = dto.Id,
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                Duration = dto.Duration.ToString(),
                Created = dto.Created.ToString()
            };
        }

        public static void UpdateFromDto(this Domain.Entities.Tour entity, UpdateTourDto dto)
        {
            entity.Name = dto.Name ?? entity.Name;
            entity.Description = dto.Description ?? entity.Description;
            entity.Price = dto.Price != default ? dto.Price : entity.Price;
        }

    }
}
