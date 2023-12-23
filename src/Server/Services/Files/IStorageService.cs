using Blanche.Domain.Files;

namespace Blanche.Server.Services.Files
{
    public interface IStorageService
    {
        Uri BasePath { get; }
        Uri GenerateImageUploadSas(ImageFile image);
    }
}
