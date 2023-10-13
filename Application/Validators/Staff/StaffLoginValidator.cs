using Infrastructure.DTOs.Request.Staff;
using FluentValidation;

namespace Application.Validators.Staff
{
    public class StaffLoginValidator : AbstractValidator<StaffLoginRequest>
    {
        public StaffLoginValidator()
        {
            RuleFor(s => s.Username).NotEmpty().WithMessage("Username cannot be empty");
            RuleFor(s => s.Password).NotEmpty().WithMessage("Password cannot be empty");
            RuleFor(s => s.Password).MinimumLength(6).WithMessage("Password length must be six or more characters");
        }
    }
}
