using ECom.Services.Ordering.App.Application.Queries;
using ECom.Services.Ordering.Domain.AggregateModels.OrderAggregate;
using ECom.Services.Ordering.Infrastructure;

namespace Ordering.Tests.Unit.Application
{
    public class GetOrdersFromCustomerQueryHandlerTest
    {
        private readonly OrderRepository _repository;

        public GetOrdersFromCustomerQueryHandlerTest()
        {
            var mediator = new Mock<IMediator>();
            _repository = new OrderRepository(mediator.Object);
        }

        [Fact]
        public void Get_orders_from_customer_query_get_result_empty()
        {
            //Arrange
            var handler = new GetOrdersFromCustomerQueryHandler(_repository);
            var query = new GetOrdersFromCustomerQuery(1);
            //Act
            _repository.Clear();
            var result = handler.Handle(query, default).Result;
            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Get_orders_from_customer_query_get_correct_empty()
        {
            //Arrange
            var handler = new GetOrdersFromCustomerQueryHandler(_repository);
            var query = new GetOrdersFromCustomerQuery(1);
            var expectedResult = 10;
            //Act
            _repository.Clear();
            for (int i = 0; i < expectedResult; i++)
            {
                _repository.Add(new OrderBuilder(new Address("Fake street " + i, "Fake city " + i)).Build());
            }
            var result = handler.Handle(query, default).Result;
            //Assert
            Assert.Equal(expectedResult, result.Count());
        }

        [Fact]
        public void Invalid_customer_id()
        {
            //Arrange
            var handler = new GetOrdersFromCustomerQueryHandler(_repository);
            var query = new GetOrdersFromCustomerQuery(-1);
            //Act
            var result = handler.Handle(query, default).Result;
            //Assert
            Assert.Empty(result);
        }
    }
}
