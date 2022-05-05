using FPTS.FIT.BDRD.Services.Catalog.App.Application.DTOs;
using MediatR;

namespace FPTS.FIT.BDRD.Services.Catalog.App.Application.Queries
{
    public class GetCatalogsQuery : IRequest<IEnumerable<CatalogDTO>>
    {
    }
}
