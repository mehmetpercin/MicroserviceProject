using Microsoft.AspNetCore.Mvc;
using Services.Basket.Dtos;
using Services.Basket.Services;
using SharedLibrary.Controllers;
using SharedLibrary.Services;

namespace Services.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : CustomBaseController
    {
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _identityService;

        public BasketsController(IBasketService basketService, ISharedIdentityService identityService)
        {
            _basketService = basketService;
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            return CreateActionResultInstance(await _basketService.GetBasket(_identityService.GetUserId));
        }

        [HttpPost]
        public async Task<IActionResult> SaveBasket(BasketDto basket)
        {
            basket.UserId = _identityService.GetUserId;
            return CreateActionResultInstance(await _basketService.SaveBasket(basket));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket()
        {
            return CreateActionResultInstance(await _basketService.DeleteBasket(_identityService.GetUserId));
        }
    }
}
