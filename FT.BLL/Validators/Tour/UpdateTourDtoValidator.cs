using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT.BLL.Validators.Tour
{
    public class UpdateTourDtoValidator : FluentValidation.AbstractValidator<FT.BLL.DTOs.TourDtos.UpdateTourDto>
    {
        public UpdateTourDtoValidator()
        {
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("El precio debe ser mayor que cero.");

            RuleFor(x => x.Name).NotEmpty().WithMessage("El nombre no puede estar vacío.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");


            RuleFor(x => x.Description).NotEmpty().WithMessage("La descripción no puede estar vacía.");
        }
    }
}
