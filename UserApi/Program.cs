using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using UserApi.Data;
using UserApi.Repositories;
using UserApi.Repositories.IRepo;
using UserApi.Service;
using UserApi.Service.IService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add postgre connect
builder.Services.AddDbContext<AppDBContext>(options =>

    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
//Đăng kí Repository
builder.Services.AddScoped<iUserRepo, UserRepo>();

// Đăng ký Service
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

//Đăng kí cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/api/auth/login";
                    options.LogoutPath = "/api/auth/logout";
                    options.AccessDeniedPath = "/api/auth/accessdenied";
                });

// Thêm Authorization nếu cần policy dựa trên claims
builder.Services.AddAuthorization();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Add exception handling middleware
app.UseExceptionHandler("/Error");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
