using SharedLibrary.Dtos;

namespace Services.Discount.Services
{
    public interface IDiscountService
    {
        Task<Response<List<Models.Discount>>> GetAll();
        Task<Response<Models.Discount>> GetById(int id);
        Task<Response<object>> Save(Models.Discount discount);
        Task<Response<object>> Update(Models.Discount discount);
        Task<Response<object>> Delete(int id);
        Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId);
    }
}
