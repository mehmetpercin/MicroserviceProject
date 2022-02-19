using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Controllers;
using SharedLibrary.Dtos;

namespace Services.Payment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : CustomBaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment()
        {
            return CreateActionResultInstance(Response<object>.Success(200));
        }
    }
}
