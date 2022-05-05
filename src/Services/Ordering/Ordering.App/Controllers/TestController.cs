using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FPTS.FIT.BDRD.Services.Ordering.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IOrderRepository _repository;
        private readonly IMediator _mediator;
        private readonly OrderDbContext _context;

        public TestController(IOrderRepository repository, IMediator mediator, OrderDbContext context)
        {
            _repository = repository;
            _mediator   = mediator;
            _context = context;
        }

        [HttpPost("demo")]
        public async Task<IActionResult> Demo()
        {
            List<OrderItemDTO> list = new();
            for (var i = 1; i < 4; i++)
            {
                list.Add(new OrderItemDTO()
                {
                    Discount = i,
                    PictureUrl = "demo",
                    ProductId = i,
                    ProductName = "ProductName>i",
                    UnitPrice = i,
                    Units = i * 10
                });
            }
            CreateOrderCommand command = new(list, 1, "demo", "demo");
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult TestAddOrder()
        {
            Random random = new Random();
            for(int i = 0; i < 10; i++)
            {
                var address = new Address(i + " - Street", i+" - City");
                // Tạo Order
                var order = new Order((i % 2 == 0) ? 1: 2, address);
                // Thêm item vào order
                var itemCount = random.Next(2, 5);
                for (int j = 0; j < itemCount; j++)
                {
                    order.AddOrderItem(j, i+" - Proname", (j+1)*1000, 0, "", random.Next(5, 10));
                }
                order.ClearDomainEvents();
                _repository.Add(order);
            }
            return Ok();
        }

        [HttpGet("DbContext")]
        public IActionResult TestDbContext()
        {
            return Ok(_context.Orders);
        }
    }
}
