using ProductsApi.Data;
using Microsoft.EntityFrameworkCore;
using ProductsApi.Repo.IRepo;
using ProductsApi.Repo;
using ProductsApi.Service.IService;
using ProductsApi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// THÊM CORS VÀO BE
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()
              .WithExposedHeaders("*"); // Thêm dòng này
    });
});
//add postgre connect
builder.Services.AddDbContext<AppDb>(options =>

    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")).UseSnakeCaseNamingConvention());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add repo 
builder.Services.AddScoped<IProductsRepo, ProductsRepo>();
// add service

builder.Services.AddScoped<IProductsService, ProductsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
