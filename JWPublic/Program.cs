using JWPublic.Services;

var builder = WebApplication.CreateBuilder(args);
// Đăng ký HttpClient và ProductService
builder.Services.AddHttpClient<ProductService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7114/"); // Thay bằng port API của bạn
    client.Timeout = TimeSpan.FromSeconds(30);
});
// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
