using eStoreClient.Service;
using eStoreClient.Services;
using eStoreClient.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace eStoreClient.Utilities
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(opt =>
                {
                    opt.ExpireTimeSpan = TimeSpan.FromHours(1);
                    opt.LoginPath = "/Auth/Login";
                    opt.AccessDeniedPath = "/Auth/AccessDenied";
                });
            return services.AddScoped<ITokenProvider, TokenProvider>()
                .AddScoped<IBaseService, BaseService>()
                .AddScoped<IAuthService, AuthService>()
                .AddScoped<IProductService, ProductService>()
                .AddScoped<ICategoryService, CategoryService>()
                .AddScoped<IMemberService, MemberService>()
                .AddScoped<IOrderService, OrderService>();

        }
    }
}
