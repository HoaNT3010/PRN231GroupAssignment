using FluentValidation;
using Infrastructure.DTOs.Request.Wallet;

namespace Application.Validators.Wallet
{
    public class WalletRechargeWithCashValidator : AbstractValidator<WalletBalanceRechargeRequest>
    {
        public WalletRechargeWithCashValidator()
        {
            RuleFor(w => w.Amount).GreaterThan(1000).WithMessage("Amount of money in recharge request must greater than 1000 VND");
        }
    }
}
