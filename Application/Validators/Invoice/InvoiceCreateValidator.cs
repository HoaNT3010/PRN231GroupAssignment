using FluentValidation;
using Infrastructure.DTOs.Request.Invoice;

namespace Application.Validators.Invoice
{
    public class InvoiceCreateValidator : AbstractValidator<InvoiceCreateRequest>
    {
        public InvoiceCreateValidator()
        {
            RuleFor(i => i.Products).NotEmpty().WithMessage("Invoice must contain at least one item");
            RuleForEach(i => i.Products).SetValidator(new InvoiceDetailCreateValidator());
        }
    }

    public class InvoiceDetailCreateValidator : AbstractValidator<InvoiceDetailCreateRequest>
    {
        public InvoiceDetailCreateValidator()
        {
            RuleFor(d => d.ProductId).GreaterThan(0).WithMessage("Invoice detail's product Id must be greater than 0");
            RuleFor(d => d.Quantity).GreaterThan(0).WithMessage("Invoice detail's quantity must be greater than 0");
        }
    }
}
