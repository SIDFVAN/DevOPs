namespace Blanche.Server.Services.Util
{
    public interface IHtmlGenerationService
    {
        Task<string> Generate<T>(string template, T data);
    }
}
