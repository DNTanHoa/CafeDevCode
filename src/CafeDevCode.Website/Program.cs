using CafeDevCode.Database;
using CafeDevCode.Database.Entities;
using CafeDevCode.Database.Seeders;
using CafeDevCode.Logic;
using CafeDevCode.Logic.Commands.Request;
using CafeDevCode.Logic.MappingProfile;
using CafeDevCode.Logic.Shared.Configs;
using CafeDevCode.Ultils.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCookiesAuthenticate(builder.Configuration);
builder.Services.AddSqlServerDatabase<AppDatabase>(builder.Configuration
    .GetConnectionString("Database"));
builder.Services.AddIdentityConfig<User, IdentityRole, AppDatabase>();
builder.Services.AddMediatR(typeof(Login).Assembly);
builder.Services.AddAutoMapper(typeof(AuthorMappingProfile).Assembly);
builder.Services.AddQueries();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.LoginPath = builder.Configuration.GetSection("AuthCookies").GetSection("LoginPath").Value;
    options.AccessDeniedPath = builder.Configuration.GetSection("AuthCookies").GetSection("AccessDeniedPath").Value;
});

builder.Services.AddGoogleAuthenticate(builder.Configuration);

builder.Services.AddAuthorization(o =>
{
    o.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.Configure<FileSystemConfig>(
    builder.Configuration.GetSection(FileSystemConfig.ConfigName));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var database = scope.ServiceProvider.GetRequiredService<AppDatabase>();
    await database.Database.MigrateAsync();
    await AppSeeder.InitializeAsync(database, userManager, roleManager);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();