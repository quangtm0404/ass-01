using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Mappers;
using Repositories.Repositories;
using Services.Repositories;
using Services.Services;
using Services.Services.Interfaces;

namespace Repositories
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection service, string dbConnection)
        {
            service.AddAutoMapper(typeof(MapperConfigurationsProfile).Assembly);
            // DI REPO
            service.AddScoped<ICategoryRepository, CategoryRepository>()
                .AddScoped<IMemberRepository, MemberRepository>()
                .AddScoped<IOrderDetailRepository, OrderDetailRepository>()
                .AddScoped<IOrderRepository, OrderRepository>()
                .AddScoped<IProductRepository, ProductRepository>();

            // DI Services
            service.AddScoped<ICategoryService, CategoryService>()
                .AddScoped<IMemberService, MemberService>()
                .AddScoped<IOrderDetailService, OrderDetailService>()
                .AddScoped<IOrderService, OrderService>()
                .AddScoped<IProductService, ProductService>();


            return service.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(dbConnection))
                .AddScoped<ITokenGenerator, TokenGenerator>();


        }
    }
}
