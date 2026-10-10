using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PizzaStar.Data;
using PizzaStar.Interfaces;
using PizzaStar.Models;
using PizzaStar.Repositories;

var builder = WebApplication.CreateBuilder(args);
IConfigurationRoot _confString = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(_confString.GetConnectionString("DefaultConnection")));



builder.Services.AddIdentity<User, IdentityRole>(opts =>
{
    opts.Password.RequiredLength = 5;   // мінімальна довжина
    opts.Password.RequireNonAlphanumeric = false;   //чи потрібні алфавітно-цифрові символи
    opts.Password.RequireLowercase = false; // чи потрібні символи в нижньому регістрі
    opts.Password.RequireUppercase = false; //чи потрібні символи у верхньому регістрі
    opts.Password.RequireDigit = false; //чи потрібні цифри
})
    .AddEntityFrameworkStores<ApplicationContext>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICategory, CategoryRepository>();
builder.Services.AddScoped<IProduct, ProductRepository>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

var app = builder.Build();
app.UseSession();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var applicationContext = services.GetRequiredService<ApplicationContext>();
        applicationContext.Database.EnsureDeleted();
        applicationContext.Database.EnsureCreated();
        var userManager = services.GetRequiredService<UserManager<User>>();
        var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await DbInit.InitializeAsync(userManager, rolesManager);

        DbInit.InitializeContentAsync(applicationContext).Wait();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
