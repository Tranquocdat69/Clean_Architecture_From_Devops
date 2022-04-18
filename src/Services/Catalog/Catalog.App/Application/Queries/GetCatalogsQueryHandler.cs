<<<<<<< HEAD
﻿using FPTS.FIT.BDRD.Services.Catalog.App.Application.DTOs;
using MediatR;

namespace FPTS.FIT.BDRD.Services.Catalog.App.Application.Queries
=======
﻿using ECom.Services.Catalog.App.Application.DTOs;
using MediatR;

namespace ECom.Services.Catalog.App.Application.Queries
>>>>>>> bcad93d (change customer to balance service + validator behavior)
{
    public class GetCatalogsQueryHandler : IRequestHandler<GetCatalogsQuery, IEnumerable<CatalogDTO>>
    {
        public Task<IEnumerable<CatalogDTO>> Handle(GetCatalogsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
