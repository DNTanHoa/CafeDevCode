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
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Compression;
using System.Text.Encodings.Web;
using System.Text.Unicode;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCookiesAuthenticate(builder.Configuration);
builder.Services.AddSqlServerDatabase<AppDatabase>(builder.Configuration
    .GetConnectionString("Database"));
builder.Services.AddIdentityConfig<User, IdentityRole, AppDatabase>();
builder.Services.AddMediatR(typeof(Login).Assembly);
builder.Services.AddAutoMapper(typeof(AuthorMappingProfile).Assembly);
builder.Services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.All }));

builder.Services.AddResponseCompression(o =>
{
    o.EnableForHttps = true;
    o.Providers.Add<BrotliCompressionProvider>();
    o.Providers.Add<GzipCompressionProvider>();
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.SmallestSize;
});

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
builder.Services.Configure<SiteConfig>(
    builder.Configuration.GetSection(SiteConfig.ConfigName));
builder.Services.Configure<ContactInfoConfig>(
    builder.Configuration.GetSection(ContactInfoConfig.ConfigName));
builder.Services.Configure<MailConfig>(
    builder.Configuration.GetSection(MailConfig.ConfigName));

builder.Services.AddFluentEmail(builder.Configuration["MailConfig:DefaultToMailAddress"])
        .AddSmtpSender(builder.Configuration["MailConfig:Host"], int.Parse(builder.Configuration["MailConfig:Port"]),
        builder.Configuration["MailConfig:UserName"], builder.Configuration["MailConfig:Password"]);


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
app.UseStaticFiles(new StaticFileOptions()
{
    OnPrepareResponse = (context) =>
    {
        if (!string.IsNullOrEmpty(context.Context.Request.Query["v"]))
        {
            context.Context.Response.Headers.Add("cache-control", new[] { "public,max-age=31536000" });
            context.Context.Response.Headers.Add("Expires", new[] { DateTime.UtcNow.AddHours(1).ToString("R") }); // Format RFC112
        }
    }
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(e =>
{
    e.MapControllerRoute(
        name: "sitemap",
        pattern: "site-map/sitemap.xml",
        defaults: new { controller = "Home", action = "SiteMap" });
    e.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();