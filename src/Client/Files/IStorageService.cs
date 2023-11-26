using Microsoft.AspNetCore.Components.Forms;

namespace Blanche.Client.Files
{
    public interface IStorageService
    {
        Task UploadImageAsync(string sas, IBrowserFile file);

        Task DeleteImageAsync(string sas);
    }
}
