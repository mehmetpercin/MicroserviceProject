using Services.Basket.Dtos;
using SharedLibrary.Dtos;

namespace Services.Basket.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string userId);
        Task<Response<object>> SaveBasket(BasketDto basket);
        Task<Response<object>> DeleteBasket(string userId);
    }
}
