using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http;

namespace Blanche.Client.Files
{
    public class BlobStorageService : IStorageService
    {
        private HttpClient _httpClient;
        public static long MaxFileSize => 1024 * 1024 * 10; // 10MB
        public BlobStorageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task UploadImageAsync(string sas, IBrowserFile file)
        {
            var content = new StreamContent(file.OpenReadStream(MaxFileSize));
            content.Headers.Add("x-ms-blob-type", "BlockBlob");
            content.Headers.Add("Content-Type", file.ContentType);
            var response = await _httpClient.PutAsync(sas, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteImageAsync(string sas)
        {
            var response = await _httpClient.DeleteAsync(sas);
            response.EnsureSuccessStatusCode();
        }
    }
}
