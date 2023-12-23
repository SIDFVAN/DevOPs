using Blanche.Shared.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Blanche.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BeerController : ControllerBase
    {
        private readonly IBeerService beerService;

        public BeerController(IBeerService beerService)
        {
            this.beerService = beerService;
        }

        [SwaggerOperation("Returns a list of beers available to choose.")]
        [HttpGet, AllowAnonymous]
        public async Task<IEnumerable<BeerDto>> GetIndex()
        {
            return await beerService.GetAllAsync();
        }

        [SwaggerOperation("Returns a specific beer.")]
        [HttpGet("{beerId}"), AllowAnonymous]
        public async Task<BeerDto> GetById(Guid beerId)
        {
            return await beerService.GetByIdAsync(beerId);
        }

        [SwaggerOperation("Creates a new beer to choose from.")]
        [HttpPost] //, Authorize(Roles = Roles.Administrator)
        public async Task<IActionResult> Create(BeerDto beer)
        {
            var beerId = await beerService.CreateAsync(beer);
            return CreatedAtAction(nameof(Create), beerId);
        }

        [SwaggerOperation("Changes can be made to an existing beer.")]
        [HttpPut] // , Authorize(Roles = Roles.Administrator)
        public async Task<IActionResult> Edit(BeerDto beer)
        {
            await beerService.UpdateAsync(beer);
            return NoContent();
        }

        [SwaggerOperation("Deletes an existing beer.")]
        [HttpDelete("{formulaId}")]// , Authorize(Roles = Roles.Administrator)
        public async Task<IActionResult> Delete(Guid formulaId)
        {
            await beerService.DeleteAsync(formulaId);
            return NoContent();  // , Authorize(Roles = Roles.Administrator)
        }
    }
}
