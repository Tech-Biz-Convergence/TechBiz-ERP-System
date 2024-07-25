using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Service;
using Microsoft.Extensions.FileSystemGlobbing.Internal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure session state
builder.Services.AddSession(options =>
{
  options.IdleTimeout = TimeSpan.FromMinutes(30); // Set timeout for session
  options.Cookie.HttpOnly = true;
  options.Cookie.IsEssential = true;
});

// Add HttpClient service
builder.Services.AddHttpClient();

// Add configuration
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddEnvironmentVariables();

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

// Add this line to enable session
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=LoginBasic}/{id?}");
    //pattern: "{controller=Dashboards}/{action=Index}/{id?}");
    // pattern: "{controller=EmployeeManagement}/{action=Index}/{id?}");

app.Run();
