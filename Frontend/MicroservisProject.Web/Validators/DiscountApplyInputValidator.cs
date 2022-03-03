using FluentValidation;
using MicroservisProject.Web.Models.Discount;

namespace MicroservisProject.Web.Validators
{
    public class DiscountApplyInputValidator : AbstractValidator<DiscountApplyInput>
    {
        public DiscountApplyInputValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("Kupon kodu boş olamaz");
        }
    }
}
