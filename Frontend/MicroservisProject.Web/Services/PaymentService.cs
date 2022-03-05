using MicroservisProject.Web.Models.Payment;
using MicroservisProject.Web.Services.Interfaces;

namespace MicroservisProject.Web.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput)
        {
            var response = await _httpClient.PostAsJsonAsync("payments", paymentInfoInput);
            return response.IsSuccessStatusCode;
        }
    }
}
