namespace ECom.Services.Ordering.App.Application.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        public Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
