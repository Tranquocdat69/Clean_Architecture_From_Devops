using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECom.Services.Ordering.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IOrderRepository _repository;

        public TestController(IOrderRepository repository)
        {
            _repository = repository;
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
    }
}
