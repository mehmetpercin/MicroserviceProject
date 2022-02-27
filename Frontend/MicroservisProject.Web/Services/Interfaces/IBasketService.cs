using MicroservisProject.Web.Models.Basket;

namespace MicroservisProject.Web.Services.Interfaces
{
    public interface IBasketService
    {
        Task<bool> SaveBasket(BasketViewModel basketViewModel);
        Task<BasketViewModel> GetBasket();
        Task<bool> DeleteBasket();
        Task<bool> AddBasketItem(BasketItemViewModel basketItemViewModel);
        Task<bool> RemoveBasketItem(string courseId);
        Task<bool> ApplyDiscount(string discountCode);
        Task<bool> CancelApplyDiscount();
    }
}
