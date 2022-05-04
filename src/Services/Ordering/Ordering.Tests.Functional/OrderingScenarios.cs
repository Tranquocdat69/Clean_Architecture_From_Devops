namespace Ordering.Tests.Functional
{
    public class OrderingScenarios
        : OrderingScenarioBase
    {
        [Fact]
        public async Task Delete_order_no_order_created_bad_request_response()
        {
            using (var server = CreateServer())
            {
                var jsonData = JsonSerializer.Serialize(new DeleteOrderCommand("fakeId"));
                var request = new HttpRequestMessage(HttpMethod.Delete, Delete.DeleteOrders);
                request.Content = new StringContent(jsonData, UTF8Encoding.UTF8, "application/json");
                var response = await server.CreateIdempotentClient().SendAsync(request);

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }
    }
}
