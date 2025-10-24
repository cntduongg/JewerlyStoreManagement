using JewerlyPublicWen.Service;
using JewerlyPublicWen.Service.IService;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
// ĐĂNG KÝ HTTP CLIENT VÀ SERVICE
builder.Services.AddHttpClient<IProductsService, ProductsService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7114/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});
// THÊM CORS ĐỂ GỌI API TỪ BE
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()    // Cho phép mọi domain
              .AllowAnyMethod()    // Cho phép mọi HTTP method
              .AllowAnyHeader();   // Cho phép mọi header
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
// SỬ DỤNG CORS - THÊM DÒNG NÀY
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
