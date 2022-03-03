using MicroservisProject.Web.Models.Discount;

namespace MicroservisProject.Web.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountViewModel> GetDiscount(string code);
    }
}
