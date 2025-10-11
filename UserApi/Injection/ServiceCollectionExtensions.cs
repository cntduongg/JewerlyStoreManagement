using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Repositories;
using UserApi.Repositories.IRepo;
using UserApi.Service;
using UserApi.Service.IService;

namespace UserApi.Injection
{
    public  static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            // Đăng ký DbContext
            services.AddDbContext<AppDBContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // Đăng ký Repository
            services.AddScoped<iUserRepo, UserRepo>();

            // Đăng ký Service
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();

            // Thêm các dịch vụ khác nếu cần
            services.AddControllers();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/api/auth/login";
                    options.LogoutPath = "/api/auth/logout";
                    options.AccessDeniedPath = "/api/auth/accessdenied";
                });
            return services;
        }
    }
}
