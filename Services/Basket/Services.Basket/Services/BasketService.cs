using Services.Basket.Dtos;
using SharedLibrary.Dtos;
using System.Text.Json;

namespace Services.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task<Response<object>> DeleteBasket(string userId)
        {
            var status = await _redisService.GetDatabase().KeyDeleteAsync(userId);
            return status ? Response<object>.Success(200) : Response<object>.Fail("Basket not found", 404);
        }

        public async Task<Response<BasketDto>> GetBasket(string userId)
        {
            var existsBasket = await _redisService.GetDatabase().StringGetAsync(userId);
            if (string.IsNullOrEmpty(existsBasket))
            {
                return Response<BasketDto>.Fail("Basket not found", 404);
            }

            return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existsBasket), 200);
        }

        public async Task<Response<object>> SaveBasket(BasketDto basket)
        {
            var status = await _redisService.GetDatabase().StringSetAsync(basket.UserId, JsonSerializer.Serialize(basket));

            return status ? Response<object>.Success(201) : Response<object>.Fail("Basket could not save", 500);
        }
    }
}
