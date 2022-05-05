using MediatR;

namespace FPTS.FIT.BDRD.Services.Catalog.App.Application.Commands
{
    public class UpdateProductAvaibleStockCommandHandler : IRequestHandler<UpdateProductAvaibleStockCommand>
    {
        public Task<Unit> Handle(UpdateProductAvaibleStockCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
