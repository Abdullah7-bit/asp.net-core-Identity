using ASPNetCoreIdentity.Data;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
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



        //facebookoptions.AppId = builder.Configuration["Authentication:Facebook:AppId"];
        //facebookoptions.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
        //facebookoptions.AccessDeniedPath = "/AccessDeniedPathInfo";
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
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

app.Run();
