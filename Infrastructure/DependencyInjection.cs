using Application.Interface;
using Infrastructure.DataBase;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<ICustomerRepository, CustomerReposiory>();
            services.AddScoped<IMovingRequestRepository, MovingRequestRepository>();

            return services;
        }
    }
}
