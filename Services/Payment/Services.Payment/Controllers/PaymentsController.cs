using Microsoft.AspNetCore.Mvc;
using Services.Payment.Models;
using SharedLibrary.Controllers;
using SharedLibrary.Dtos;

namespace Services.Payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : CustomBaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment(PaymentInfoDto paymentInfoDto)
        {
            return CreateActionResultInstance(Response<object>.Success(200));
        }
    }
}
