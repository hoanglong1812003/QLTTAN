using Microsoft.EntityFrameworkCore;
using DoAnCoSo.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure database context
builder.Services.AddDbContext<QuanLyTrungTamAnhNguContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") + ";TrustServerCertificate=True"));

// Configure session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Register the custom action filter
builder.Services.AddScoped<AuthFilter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Use session
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Taikhoan}/{action=Login}/{id?}");

app.Run();
