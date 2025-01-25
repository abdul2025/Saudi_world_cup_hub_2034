using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SaudiWorldCupHub.Data;
using SaudiWorldCupHub.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});



// Configure Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddSignInManager()
    .AddUserManager<UserManager<IdentityUser>>()
    // .AddRoleManager<RoleManager<IdentityRole>>()  // Uncomment if using roles
    // .AddRoles<IdentityRole>()  // Uncomment if using roles
    .AddDefaultTokenProviders();

// Register the HttpClient and the FlightOfferService
builder.Services.AddHttpClient<FlightOffersPricingService>();


builder.Services.AddRazorPages(); // Ensure Razor Pages are added for Identity pages

var app = builder.Build();

// Make sure to use Authentication and Authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Seed the database with cities if needed
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
    CitySeeder.SeedCities(dbContext, env); // Seed cities if needed
    NationalitiesSeeder.SeedNationalities(dbContext, env); // Seed NationalitiesSeeder if needed
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

app.UseAuthorization();

app.UseSession();


app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages(); // This is required for Identity pages

app.Run();
