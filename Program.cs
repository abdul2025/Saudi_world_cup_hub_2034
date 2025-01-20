using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SaudiWorldCupHub.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add Identity
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddSignInManager()
    .AddUserManager<UserManager<IdentityUser>>()
    // .AddRoleManager<RoleManager<IdentityRole>>()
    // .AddRoles<IdentityRole>()
    .AddDefaultTokenProviders();


builder.Services.AddRazorPages(); // Ensure Razor Pages are added for Identity pages


var app = builder.Build();

// Make sure to use Authentication and Authorization middleware
app.UseAuthentication();
app.UseAuthorization();



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

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages(); // This is required for Identity pages

app.Run();
