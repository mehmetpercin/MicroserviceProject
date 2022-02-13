using Dapper;
using Npgsql;
using SharedLibrary.Dtos;
using System.Data;

namespace Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<object>> Delete(int id)
        {
            await _dbConnection.ExecuteAsync("delete from discount where id=@Id",
                            new { Id = id });

            return Response<object>.Success(204);
        }

        public async Task<Response<List<Models.Discount>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("select * from discount");
            return Response<List<Models.Discount>>.Success(discounts.ToList(), 200);
        }

        public async Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("select * from discount where code=@Code and userid=@UserId", new { Code = code, UserId = userId })).FirstOrDefault();
            if (discount == null)
            {
                return Response<Models.Discount>.Fail("Discount not found", 404);
            }
            return Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<Response<Models.Discount>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("select * from discount where id=@Id", new { Id = id })).SingleOrDefault();
            if (discount == null)
            {
                return Response<Models.Discount>.Fail("Discount not found", 404);
            }
            return Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<Response<object>> Save(Models.Discount discount)
        {
            await _dbConnection.ExecuteAsync("insert into discount(userid,rate,code) values(@UserId,@Rate,@Code)", discount);

            return Response<object>.Success(201);
        }

        public async Task<Response<object>> Update(Models.Discount discount)
        {
            await _dbConnection.ExecuteAsync("update discount set userid=@UserId,code=@Code,rate=@Rate where id=@Id",
                            new { discount.UserId, discount.Code, discount.Rate, discount.Id });

            return Response<object>.Success(204);

        }
    }
}
