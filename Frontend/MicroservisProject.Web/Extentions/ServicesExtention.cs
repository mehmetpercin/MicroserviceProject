using MicroservisProject.Web.Handlers;
using MicroservisProject.Web.Helpers;
using MicroservisProject.Web.Models;
using MicroservisProject.Web.Services;
using MicroservisProject.Web.Services.Interfaces;
using SharedLibrary.Services;

namespace MicroservisProject.Web.Extentions
{
    public static class ServicesExtention
    {
        public static void AddServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<ServiceApiSettings>(configuration.GetSection("ServiceApiSettings"));
            services.Configure<ClientSettings>(configuration.GetSection("ClientSettings"));
            var serviceApiSettings = configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();

            services.AddSingleton<PhotoHelper>();

            services.AddHttpContextAccessor();

            services.AddHttpClient<IIdentityService, IdentityService>();
            services.AddHttpClient<IUserService, UserService>(opt =>
            {
                opt.BaseAddress = new Uri(serviceApiSettings.IdentityBaseUri);
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<ICatalogService, CatalogService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Catalog.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<IPhotoStockService, PhotoStockService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.PhotoStock.Path}");
            }).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            services.AddHttpClient<IBasketService, BasketService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Basket.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddHttpClient<IDiscountService, DiscountService>(opt =>
            {
                opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Discount.Path}");
            }).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            services.AddAccessTokenManagement(); // IClientAccessTokenCache

            services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();

            services.AddScoped<ResourceOwnerPasswordTokenHandler>();
            services.AddScoped<ClientCredentialTokenHandler>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<ISharedIdentityService, SharedIdentityService>();

        }
    }
}
