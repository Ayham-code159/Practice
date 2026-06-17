using CRUDPractice.Models.Dtos.UserDtos;
using FluentValidation;

namespace CRUDPractice.Validators.UserValidators
{
    internal sealed class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
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
                

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password can't be empty.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .MaximumLength(30).WithMessage("Password can't be more than 30 characters.")
                .Matches("[A-Z]").WithMessage("Password must contain one uppercase character.")
                .Matches("[a-z]").WithMessage("Password must contain one lowercase character.")
                .Matches("[0-9]").WithMessage("Password must contain one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain one special character.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("Password doesn't match");
        }
    }
}