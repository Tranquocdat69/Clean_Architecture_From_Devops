namespace Ordering.Tests.Unit.Application
#nullable disable
{
    public class OrdersWebApiTest
    {
        private readonly Mock<IMediator> _mediatorMock;

        public OrdersWebApiTest()
        {
            _mediatorMock = new Mock<IMediator>();
        }

        [Fact]
        public async Task Delete_order_bad_request()
        {
            //Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteOrderCommand>(), default(CancellationToken)))
                .Returns(Task.FromResult(false));
            DeleteOrderCommand command = new DeleteOrderCommand("FakeId");

            //Act
            var orderController = new OrderController(_mediatorMock.Object);
            var actionResult = await orderController.DeleteOrderAsync(command) as BadRequestResult;

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Delete_order_success()
        {
            //Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteOrderCommand>(), default(CancellationToken)))
                .Returns(Task.FromResult(true));
            DeleteOrderCommand command = new DeleteOrderCommand("FakeId");

            //Act
            var orderController = new OrderController(_mediatorMock.Object);
            var actionResult = await orderController.DeleteOrderAsync(command) as OkResult;

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task Create_order_success()
        {
            //Arrange
            var fakeItems = Enumerable.Empty<OrderItemDTO>();
            ResponseData faceResult = new ResponseData() { IsSuccess = true, Message = "success"};
            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateOrderCommand>(), default(CancellationToken)))
                .Returns(Task.FromResult(faceResult));
            CreateOrderCommand command = new CreateOrderCommand(fakeItems, 1, "fake city","fake street");

            //Act
            var orderController = new OrderController(_mediatorMock.Object);
            var actionResult    = await orderController.CreateOrderAsync(command) as OkObjectResult;
            var result          = actionResult.Value as ResponseData;

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task Create_order_fail()
        {
            //Arrange
            var fakeItems = Enumerable.Empty<OrderItemDTO>();
            ResponseData faceResult = new ResponseData() { IsSuccess = false, Message = "success" };
            _mediatorMock.Setup(x => x.Send(It.IsAny<CreateOrderCommand>(), default(CancellationToken)))
                .Returns(Task.FromResult(faceResult));
            CreateOrderCommand command = new CreateOrderCommand(fakeItems, 1, "fake city", "fake street");

            //Act
            var orderController = new OrderController(_mediatorMock.Object);
            var actionResult = await orderController.CreateOrderAsync(command) as OkObjectResult;
            var result = actionResult.Value as ResponseData;

            //Assert
            Assert.Equal(actionResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
            Assert.False(result.IsSuccess);
        }
    }
}
