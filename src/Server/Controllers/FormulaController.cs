using Blanche.Shared;
using Blanche.Shared.Formulas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
//using Blanche.Shared.Authentication;


namespace Blanche.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormulaController : ControllerBase
    {
        private readonly IFormulaService formulaService;

        public FormulaController(IFormulaService formulaService)
        {
            this.formulaService = formulaService;
        }

        [SwaggerOperation("Returns a list of formulas available to choose.")]
        [HttpGet, AllowAnonymous]
        public async Task<List<FormulaDto.Index>> GetIndex()
        {
            return await formulaService.GetIndexAsync();
        }

        [SwaggerOperation("Returns a specific formula available to choose.")]
        [HttpGet("{formulaId}"), AllowAnonymous]
        public async Task<FormulaDto.Detail> GetDetail(Guid formulaId)
        {
            return await formulaService.GetDetailAsync(formulaId);
        }

        [SwaggerOperation("Creates a new formula in the options.")]
        [HttpPost] //, Authorize(Roles = Roles.Administrator)
        public async Task<IActionResult> Create(FormulaDto.Mutate model)
        {
            var formulaId = await formulaService.CreateAsync(model);
            return CreatedAtAction(nameof(Create), formulaId);
        }

        [SwaggerOperation("Edites an existing formula in the catalog.")]
        [HttpPut("{formulaId}")] // , Authorize(Roles = Roles.Administrator)
        public async Task<IActionResult> Edit(Guid formulaId, FormulaDto.Mutate model)
        {
            await formulaService.EditAsync(formulaId, model);
            return NoContent();
        }

        [SwaggerOperation("Deletes an existing formula in the catalog.")]
        [HttpDelete("{formulaId}")]
        public async Task<IActionResult> Delete(Guid formulaId)
        {
            await formulaService.DeleteAsync(formulaId);
            return NoContent();  // , Authorize(Roles = Roles.Administrator)
        }

    }
}