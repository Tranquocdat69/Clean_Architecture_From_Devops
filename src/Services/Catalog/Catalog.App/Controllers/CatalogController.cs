<<<<<<< HEAD
﻿using FPTS.FIT.BDRD.Services.Catalog.App.Application.DTOs;
using FPTS.FIT.BDRD.Services.Catalog.App.Application.Queries;
=======
﻿using ECom.Services.Catalog.App.Application.DTOs;
using ECom.Services.Catalog.App.Application.Queries;
>>>>>>> bcad93d (change customer to balance service + validator behavior)
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

<<<<<<< HEAD
namespace FPTS.FIT.BDRD.Services.Catalog.App.Controllers
=======
namespace ECom.Services.Catalog.App.Controllers
>>>>>>> bcad93d (change customer to balance service + validator behavior)
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCatalogs(GetCatalogsQuery query)
        {
            var catalogs = await _mediator.Send(query);
            return Ok(catalogs);
        }
    }
}
