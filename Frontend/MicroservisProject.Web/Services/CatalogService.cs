using MicroservisProject.Web.Helpers;
using MicroservisProject.Web.Models.Catalog;
using MicroservisProject.Web.Services.Interfaces;
using SharedLibrary.Dtos;

namespace MicroservisProject.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly IPhotoStockService _photoStockService;
        private readonly PhotoHelper _photoHelper;

        public CatalogService(HttpClient httpClient, IPhotoStockService photoStockService, PhotoHelper photoHelper)
        {
            _httpClient = httpClient;
            _photoStockService = photoStockService;
            _photoHelper = photoHelper;
        }

        public async Task<bool> CreateCategory(CategoryCreateInput categoryCreateInput)
        {
            var response = await _httpClient.PostAsJsonAsync($"categories/Create", categoryCreateInput);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateCourse(CourseCreateInput courseCreateInput)
        {
            var photoResponse = await _photoStockService.UploadPhoto(courseCreateInput.PhotoFormFile);
            if (photoResponse.Url != null)
            {
                courseCreateInput.Picture = photoResponse.Url;
            }

            var response = await _httpClient.PostAsJsonAsync($"courses/Create", courseCreateInput);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourse(string courseId)
        {
            var response = await _httpClient.DeleteAsync($"courses/Delete/{courseId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryViewModel>> GetAllCategories()
        {
            var response = await _httpClient.GetAsync("categories/GetAll");
            if (!response.IsSuccessStatusCode)
            {
                return new List<CategoryViewModel>();
            }

            var successResponse = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();
            return successResponse.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourse()
        {
            var response = await _httpClient.GetAsync("courses/GetAll");
            if (!response.IsSuccessStatusCode)
            {
                return new List<CourseViewModel>();
            }

            var successResponse = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();
            successResponse.Data.ForEach(x =>
            {
                x.Picture = _photoHelper.GetPhotoStockUrl(x.Picture);
            });

            return successResponse.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserId(string userId)
        {
            var response = await _httpClient.GetAsync($"courses/GetAllByUserId/{userId}");
            if (!response.IsSuccessStatusCode)
            {
                return new List<CourseViewModel>();
            }

            var successResponse = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();
            successResponse.Data.ForEach(x =>
            {
                x.Picture = _photoHelper.GetPhotoStockUrl(x.Picture);
            });
            return successResponse.Data;
        }

        public async Task<CategoryViewModel> GetCategoryById(string categoryId)
        {
            var response = await _httpClient.GetAsync($"categories/GetById/{categoryId}");
            if (!response.IsSuccessStatusCode)
            {
                return new CategoryViewModel();
            }

            var successResponse = await response.Content.ReadFromJsonAsync<Response<CategoryViewModel>>();
            return successResponse.Data;
        }

        public async Task<CourseViewModel> GetCourseById(string courseId)
        {
            var response = await _httpClient.GetAsync($"courses/GetById/{courseId}");
            if (!response.IsSuccessStatusCode)
            {
                return new CourseViewModel();
            }

            var successResponse = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();
            return successResponse.Data;
        }

        public async Task<bool> UpdateCourse(CourseUpdateInput courseUpdateInput)
        {
            if(courseUpdateInput.PhotoFormFile != null && courseUpdateInput.PhotoFormFile.Length > 0)
            {
                var photoResponse = await _photoStockService.UploadPhoto(courseUpdateInput.PhotoFormFile);
                if (photoResponse.Url != null)
                {
                    _photoStockService.DeletePhoto(courseUpdateInput.Picture);
                    courseUpdateInput.Picture = photoResponse.Url;
                }
            }
            var response = await _httpClient.PutAsJsonAsync($"courses/Update", courseUpdateInput);
            return response.IsSuccessStatusCode;
        }
    }
}
