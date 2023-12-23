using Blanche.Domain.Reservations;
using Blanche.Server.Services.Util;
using System.Security.Policy;

namespace Blanche.Server.Util
{
    public class PdfGeneratorService : IPdfGeneratorService
    {
     
        public IHtmlGenerationService HtmlGenerationService { get; }

        public PdfGeneratorService(IHtmlGenerationService htmlGenerationService)
        {
            HtmlGenerationService = htmlGenerationService;
        }

        public async Task<byte[]> Generate<T>(string template, T data)
        {
            var htmlContent = await HtmlGenerationService.Generate(template, data);

            return ToPdf(htmlContent);
        }

        private byte[] ToPdf(string htmlContent)
        {
            var renderer = new ChromePdfRenderer();
            
            return renderer.RenderHtmlAsPdf(htmlContent).BinaryData;
        }
    }
}
