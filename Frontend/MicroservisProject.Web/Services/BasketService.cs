using MicroservisProject.Web.Models.Basket;
using MicroservisProject.Web.Services.Interfaces;
using SharedLibrary.Dtos;

namespace MicroservisProject.Web.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;
        private readonly IDiscountService _discountService;

        public BasketService(HttpClient httpClient, IDiscountService discountService)
        {
            _httpClient = httpClient;
            _discountService = discountService;
        }

        public async Task<bool> AddBasketItem(BasketItemViewModel basketItemViewModel)
        {
            var basket = await GetBasket();

            if (basket != null && !basket.BasketItems.Any(x => x.CourseId == basketItemViewModel.CourseId))
            {
                basket.BasketItems.Add(basketItemViewModel);
                return await SaveBasket(basket);
            }

            basket = new BasketViewModel();
            basket.BasketItems.Add(basketItemViewModel);
            var result = await SaveBasket(basket);
            return result;
        }

        public async Task<bool> ApplyDiscount(string discountCode)
        {
            await CancelApplyDiscount();

            var basket = await GetBasket();
            if (basket == null)
            {
                return false;
            }

            var hasDiscount = await _discountService.GetDiscount(discountCode);
            if (hasDiscount == null)
            {
                return false;
            }

            basket.ApplyDiscount(hasDiscount.Code, hasDiscount.Rate);
            return await SaveBasket(basket);
        }

        public async Task<bool> CancelApplyDiscount()
        {
            var basket = await GetBasket();
            if (basket == null || basket.DiscountCode == string.Empty)
            {
                return false;
            }

            basket.CancelDiscount();
            return await SaveBasket(basket);
        }

        public async Task<bool> DeleteBasket()
        {
            var response = await _httpClient.DeleteAsync("baskets");
            return response.IsSuccessStatusCode;
        }

        public async Task<BasketViewModel> GetBasket()
        {
            var response = await _httpClient.GetAsync("baskets");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var basketResponse = await response.Content.ReadFromJsonAsync<Response<BasketViewModel>>();
            return basketResponse.Data;
        }

        public async Task<bool> RemoveBasketItem(string courseId)
        {
            var basket = await GetBasket();
            if (basket == null)
            {
                return false;
            }

            var deleteBasketItem = basket.BasketItems.FirstOrDefault(x => x.CourseId == courseId);
            if (deleteBasketItem == null)
            {
                return false;
            }
            var deletedResult = basket.BasketItems.Remove(deleteBasketItem);

            if (!basket.BasketItems.Any())
            {
                basket.DiscountCode = string.Empty;
            }

            return await SaveBasket(basket);
        }

        public async Task<bool> SaveBasket(BasketViewModel basketViewModel)
        {
            basketViewModel.UserId = string.Empty;
            basketViewModel.DiscountCode = basketViewModel.DiscountCode ?? string.Empty;
            basketViewModel.DiscountRate = basketViewModel.DiscountRate ?? 0;

            var response = await _httpClient.PostAsJsonAsync("baskets", basketViewModel);
            return response.IsSuccessStatusCode;
        }
    }
}
