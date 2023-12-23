using Blanche.Server.Services.Util;
using RazorLight;

namespace Blanche.Server.Util
{
    public class HtmlGeneratorService : IHtmlGenerationService
    {
        private readonly IRazorLightEngine _razorLightEngine;

        public HtmlGeneratorService(IRazorLightEngine razorLightEngine)
        {
            _razorLightEngine = razorLightEngine;
        }
        public async Task<string> Generate<T>(string template, T data)
        {
            return await _razorLightEngine.CompileRenderAsync(template, data);
        }
    }
}
