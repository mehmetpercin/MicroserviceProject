using MicroservisProject.Web.Models.Catalog;

namespace MicroservisProject.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<List<CourseViewModel>> GetAllCourse();
        Task<List<CourseViewModel>> GetAllCourseByUserId(string userId);  
        Task<CourseViewModel> GetCourseById(string courseId);
        Task<bool> CreateCourse(CourseCreateInput courseCreateInput);
        Task<bool> UpdateCourse(CourseUpdateInput courseUpdateInput);
        Task<bool> DeleteCourse(string courseId);

        Task<List<CategoryViewModel>> GetAllCategories();
        Task<CategoryViewModel> GetCategoryById(string categoryId);
        Task<bool> CreateCategory(CategoryCreateInput categoryCreateInput);
    }
}
