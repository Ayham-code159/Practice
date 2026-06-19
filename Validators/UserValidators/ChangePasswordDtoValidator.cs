using CRUDPractice.Models.Dtos.UserDtos;
using FluentValidation;

namespace CRUDPractice.Validators.UserValidators
{
    internal sealed class ChangePasswordDtoValidator: AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Current password is required.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                .MaximumLength(30).WithMessage("Password can't be more than 30 characters.")
                .Matches("[A-Z]").WithMessage("Password must contain one uppercase character.")
                .Matches("[a-z]").WithMessage("Password must contain one lowercase character.")
                .Matches("[0-9]").WithMessage("Password must contain one number.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain one special character.");
                


            RuleFor(x => x.ConfirmNewPassword)
                .Equal(x => x.NewPassword).WithMessage("Password doesn't match.");
        }




    }
}
