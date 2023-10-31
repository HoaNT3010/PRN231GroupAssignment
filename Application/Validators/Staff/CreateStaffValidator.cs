using FluentValidation;
using Infrastructure.DTOs.Request.Staff;

namespace Application.Validators.Staff
{
    public class CreateStaffValidator : AbstractValidator<StaffCreateRequest>
    {
        public CreateStaffValidator()
        {
            RuleFor(s => s.Username).NotEmpty().WithMessage("Staff's username cannot be empty")
                .MinimumLength(8).WithMessage("Staff's username must be at least 8 characters");
        }
    }
}
