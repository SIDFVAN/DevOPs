using Blanche.Domain.Formulas;
using Microsoft.AspNetCore.Mvc;

namespace Blanche.Server.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class FormulaController : ControllerBase
	{
        [HttpPost]
        public ActionResult<IEnumerable<Formula>> GetFormulas()
		{
			List<Formula> formules = new()
			{
				//new Formula("Gewoon Blanche", "Geniet van de essentie van Blanche met onze basis formule. Da's gewoon Blanche", 250),
				//new Formula("Brew Buggy", "Mobiele bierervaring op zijn best. Geniet van ambachtelijk bier, direct getapt vanuit deze stijlvolle mobiele tapinstallatie.", 550),
				//new Formula("Bier & Bite Bonanza", "Stap in de wereld van smaak. Huur de foodtruck en voeg een extra dimensie toe met een selectie van bieren naar keuze, perfect samengesteld met een heerlijke hamburger.", 750)
			};
			return formules;
			//return formuleService.GetFormulas()
		}
	}
}