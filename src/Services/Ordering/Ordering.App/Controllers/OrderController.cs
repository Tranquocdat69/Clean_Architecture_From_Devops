using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECom.Services.Ordering.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("demo")]
        public IActionResult Demo()
        {
            var demo = new UpdateProductAvaibleStockIntegration(new List<int>() { 1, 2, 3 }, "đemo", "123");
            return Ok(demo.ToString());
        }
    }
}
