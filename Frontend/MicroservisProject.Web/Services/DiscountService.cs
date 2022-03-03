using MicroservisProject.Web.Models.Discount;
using MicroservisProject.Web.Services.Interfaces;
using SharedLibrary.Dtos;

namespace MicroservisProject.Web.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient _httpClient;

        public DiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DiscountViewModel> GetDiscount(string code)
        {
            var response = await _httpClient.GetAsync($"discounts/GetByCode/{code}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var successResponse = await response.Content.ReadFromJsonAsync<Response<DiscountViewModel>>();
            return successResponse.Data;
        }
    }
}
