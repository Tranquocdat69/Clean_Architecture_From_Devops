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
    public class GetCatalogsQuery : IRequest<IEnumerable<CatalogDTO>>
    {
    }
}
