using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FPTS.FIT.BDRD.Services.Ordering.App.Controllers
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

        [HttpDelete]
        public async Task<IActionResult> DeleteOrderAsync([FromBody] DeleteOrderCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
