using FluentValidation;
using FT.BLL.DTOs.TourDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FT.BLL.Validators.Tour
{
    public class CreateTourDtoValidator : AbstractValidator<CreateTourDto>
    {
        public CreateTourDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre del tour es obligatorio.")
                .MaximumLength(200).WithMessage("El nombre no puede tener más de 200 caracteres.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a 0.")
                .LessThanOrEqualTo(10000000).WithMessage("El precio no puede superar los 10.000.000");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("La descripción es obligatoria.")
                .MaximumLength(1000).WithMessage("La descripción no puede superar los 1000 caracteres.");
        }


    }
}
