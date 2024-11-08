using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhunnyShop.Data;
using PhunnyShop.Models;
using PhunnyShop.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<UserService>();

// Add services to the container.
builder.Services.AddControllersWithViews();
    // ApplicationDBContext.cs in folder 'Data'. DefaultConnection comes from Connection String.
builder.Services.AddDbContext<ApplicationDBContext>(options=> options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<ApplicationDBContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/LoginRegister";  // Redirects to the login page if not authenticated
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();