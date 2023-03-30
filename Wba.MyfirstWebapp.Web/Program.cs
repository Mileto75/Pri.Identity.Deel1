using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pri.Ca.Core.Entities;
using Pri.Ca.Core.Interfaces.Repositories;
using Pri.Ca.Core.Interfaces.Services;
using Pri.Ca.Core.Services;
using Pri.Ca.Infrastructure.Data;
using Pri.Ca.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

//Register dbContext
builder.Services.AddDbContext<ApplicationDbcontext>
    (options => options
    .UseSqlServer(builder.Configuration.GetConnectionString("GamesDb")));

//register IdentityContext
builder.Services.AddDefaultIdentity<ApplicationUser>(
    options =>
    {
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 3;
        options.SignIn.RequireConfirmedAccount = false;
    }
    )
    .AddEntityFrameworkStores<ApplicationDbcontext>();
// Add services to the container.
//register repositories
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
//register services
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
