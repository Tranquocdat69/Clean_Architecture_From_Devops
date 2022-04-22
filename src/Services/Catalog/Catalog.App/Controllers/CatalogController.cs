using FPTS.FIT.BDRD.Services.Catalog.App.Application.DTOs;
using FPTS.FIT.BDRD.Services.Catalog.App.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FPTS.FIT.BDRD.Services.Catalog.App.Controllers
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
