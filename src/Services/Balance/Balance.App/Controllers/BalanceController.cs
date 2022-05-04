using Confluent.Kafka;
using ECom.Services.Balance.App.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Balance.App.Controllers;

[ApiController]
public class BalanceController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IPublisher<ProducerData<string, string>> _producer;
    private const string Topic = "balance-command-topic";
    public BalanceController(IPublisher<ProducerData<string, string>> producer, IMediator mediator)
    {
        _mediator = mediator;
        _producer = producer;
    }

    [HttpGet]
    [Route("/GetUserBalanceByUserId/{userId}")]
    public async Task<IActionResult> GetUserBalanceByUserId(int userId)
    {
        var query = new GetUserBalanceByUserIdQuery()
        {
            UserId = userId
        };
        var userDTO = await _mediator.Send(query);
        if (userDTO is not null)
        {
            return Ok(userDTO);
        }
        return BadRequest("User does not exist");
    }

    [HttpPost]
    [Route("Publish-Command-Single-Message/{userId}/{totalCost}")]
    public IActionResult PostCommandSingleMsg(int userId, decimal totalCost)
    {
        var keyMsg = "command" + Guid.NewGuid().ToString();
        var valueMsg = "{\"TotalCost\": " + totalCost + ",\"UserId\": " + userId + ",\"ReplyAddress\": \"localhost:8888\"}";

        var producerData = new ProducerData<string, string>(valueMsg, keyMsg, Topic, 0);
        _producer.Publish(producerData);

        return Ok();
    }

    [HttpPost]
    [Route("Publish-Compensate-Single-Message/{userId}/{totalCost}")]
    public IActionResult PostCompensateSingleMsg(int userId, decimal totalCost)
    {
        var keyMsg = "compensate" + Guid.NewGuid().ToString();
        var valueMsg = "{\"TotalCost\": " + totalCost + ",\"UserId\": " + userId + ",\"ReplyAddress\": \"localhost:8888\"}";

        var producerData = new ProducerData<string, string>(valueMsg, keyMsg, Topic, 0);
        _producer.Publish(producerData);

        return Ok();
    }

    [HttpPost]
    [Route("Publish-Multi-Message")]
    public IActionResult PostMultiMsg()
    {
        for (int i = 1; i <= 1000; i++)
        {
            var keyMsg = "command" + Guid.NewGuid().ToString();
            var valueMsg = "{\"TotalCost\": " + 1000 + ",\"UserId\": " + i + ",\"ReplyAddress\": \"localhost:8888\"}";

            var producerData = new ProducerData<string, string>(valueMsg, keyMsg, Topic, 0);
            _producer.Publish(producerData);
        }

        return Ok();
    }

    /*[HttpPost]
    [Route("Publish-Single-Message-Catalog")]
    public IActionResult PostSingleMsgCatalog()
    {
            var keyMsg = "command" + Guid.NewGuid().ToString();
            var valueMsg = "{\"Id\": " + 1 + ",\"Quantity\": " + 10 + ",\"ReplyAddress\": \"localhost:8888\"}";

            var producerData = new ProducerData<string, string>(valueMsg, keyMsg, "catalog-command-topic", 0);
            _producer.Publish(producerData);

        return Ok();
    }*/
}
