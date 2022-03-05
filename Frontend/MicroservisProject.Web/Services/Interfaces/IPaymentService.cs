using MicroservisProject.Web.Models.Payment;

namespace MicroservisProject.Web.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput);
    }
}
