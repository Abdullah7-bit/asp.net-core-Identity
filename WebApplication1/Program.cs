using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("WebApplication1ContextDbConnection") ?? throw new InvalidOperationException("Connection string 'WebApplication1ContextDbConnection' not found.");
var config = builder.Configuration;

builder.Services.AddDbContext<WebApplication1ContextDb>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddDefaultTokenProviders()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<WebApplication1ContextDb>();

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddAuthentication()
    .AddFacebook(facebookoptions =>
    {
        IConfigurationSection FBAuthNSection =
        config.GetSection("Authentication:Facebook");
        facebookoptions.AppId = FBAuthNSection["AppId"];
        facebookoptions.AppSecret = FBAuthNSection["AppSecret"];
        facebookoptions.CallbackPath = "/signin-facebook";
        facebookoptions.AccessDeniedPath = "/AccessDeniedPathInfo";
    })
    .AddTwitter(twitteroptions =>
    {
        twitteroptions.ConsumerKey = builder.Configuration["Authentication:Twitter:ConsumerAPIKey"];
        twitteroptions.ConsumerSecret = builder.Configuration["Authentication:Twitter:ConsumerSecret"];
        twitteroptions.RetrieveUserDetails = true;
        twitteroptions.CallbackPath = "/signin-twitter";
    });

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

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
