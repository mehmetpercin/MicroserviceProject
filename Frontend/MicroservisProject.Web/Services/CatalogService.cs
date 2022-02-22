using MicroservisProject.Web.Models.Catalog;
using MicroservisProject.Web.Services.Interfaces;
using SharedLibrary.Dtos;

namespace MicroservisProject.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateCategory(CategoryCreateInput categoryCreateInput)
        {
            var response = await _httpClient.PostAsJsonAsync($"categories/Create",categoryCreateInput);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> CreateCourse(CourseCreateInput courseCreateInput)
        {
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
            var response = await _httpClient.PutAsJsonAsync($"courses/Update", courseUpdateInput);
            return response.IsSuccessStatusCode;
        }
    }
}
