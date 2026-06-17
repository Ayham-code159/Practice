using CRUDPractice.Models.Dtos.UserDtos;
using FluentValidation;

namespace CRUDPractice.Validators.UserValidators
{
    internal sealed class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {

            RuleFor(x => x.FirstName)
             .NotEmpty().WithMessage("First name can't be empty.")
             .MinimumLength(3).WithMessage("First name must be at least 3 characters.")
             .MaximumLength(50).WithMessage("First name can't be more than 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name can't be empty.")
                .MinimumLength(3).WithMessage("Last name must be at least 3 characters.")
                .MaximumLength(50).WithMessage("Last name can't be more than 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email can't be empty.")
                .EmailAddress().WithMessage("Wrong email format.");


            RuleFor(x => x.Age)
                .InclusiveBetween(18, 100)
                .WithMessage("Age must be between 18 and 100.");

           



        }




    }
}
