using MicroservisProject.Web.Models.PhotoStock;
using MicroservisProject.Web.Services.Interfaces;
using SharedLibrary.Dtos;

namespace MicroservisProject.Web.Services
{
    public class PhotoStockService : IPhotoStockService
    {
        private readonly HttpClient _httpClient;

        public PhotoStockService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> DeletePhoto(string photoUrl)
        {
            var response = await _httpClient.DeleteAsync($"photos?photoUrl={photoUrl}");
            return response.IsSuccessStatusCode;
        }

        public async Task<PhotoViewModel> UploadPhoto(IFormFile photo)
        {
            if (photo == null || photo.Length <= 0)
            {
                return new PhotoViewModel();
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";
            using var ms = new MemoryStream();
            await photo.CopyToAsync(ms);
            var multiPartContent = new MultipartFormDataContent();
            multiPartContent.Add(new ByteArrayContent(ms.ToArray()), "photo", fileName);
            var response = await _httpClient.PostAsync("photos", multiPartContent);
            if (!response.IsSuccessStatusCode)
            {
                return new PhotoViewModel();
            }

            var successResponse = await response.Content.ReadFromJsonAsync<Response<PhotoViewModel>>();
            return successResponse.Data;
        }
    }
}
