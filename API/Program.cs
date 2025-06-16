using Application.Interface;
using Infrastructure.DataBase;
using Infrastructure.Repositories;
using Infrastructure.Validator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Load configuration
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            // Database context
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Dependency Injection - Repositories
            builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
            builder.Services.AddScoped<ICustomerRepository, CustomerReposiory>();
            builder.Services.AddScoped<IMovingRequestRepository, MovingRequestRepository>();


            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the middleware pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication(); // Ensure authentication comes before authorization
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

    }
}
