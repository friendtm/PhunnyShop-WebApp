using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PhunnyShop.Data;
using PhunnyShop.Models;
using PhunnyShop.Services;

var builder = WebApplication.CreateBuilder(args);

// Add UserService class
builder.Services.AddScoped<UserService>();

// Add services to the container.
builder.Services.AddControllersWithViews();
    // ApplicationDBContext.cs in folder 'Data'. DefaultConnection comes from Connection String.
builder.Services.AddDbContext<ApplicationDBContext>(options=> options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity Password options
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

// Configure Route When Not Authenticated
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/LoginRegister";  // Redirects to the login page if not authenticated
    options.AccessDeniedPath = "/Home/Index"; // Optional: page to show when access is denied
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await SeedData.Initialize(roleManager, userManager);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Generate Admin Role
public static class SeedData
{
    public static async Task Initialize(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        // Create Admin role if it doesn't exist
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        // Optionally, create an admin user and assign to Admin role
        var adminUser = await userManager.FindByEmailAsync("admin@gmail.com");
        if (adminUser == null)
        {
            adminUser = new ApplicationUser { UserName = "admin@gmail.com", Email = "admin@gmail.com", Contact = "000000000", FirstName = "Admin", LastName = "Sal" };
            await userManager.CreateAsync(adminUser, "12345");
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}