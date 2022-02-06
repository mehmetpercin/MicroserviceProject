using Services.Catalog.Dtos;
using SharedLibrary.Dtos;

namespace Services.Catalog.Services
{
    public interface ICourseService
    {
        Task<Response<List<CourseDto>>> GetAllAsync();
        Task<Response<CourseDto>> GetByIdAsync(string id);
        Task<Response<List<CourseDto>>> GetByUserIdAsync(string userId);
        Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseCreateDto);
        Task<Response<object>> UpdateAsync(CourseUpdateDto courseUpdateDto);
        Task<Response<object>> DeleteAsync(string id);
    }
}
