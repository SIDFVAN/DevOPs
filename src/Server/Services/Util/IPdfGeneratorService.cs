namespace Blanche.Server.Services.Util
{
    public interface IPdfGeneratorService
    {

        Task<byte[]> Generate<T>(string template, T data);

    }
}
