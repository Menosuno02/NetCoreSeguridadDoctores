using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NetCoreSeguridadDoctores.Data;
using NetCoreSeguridadDoctores.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

// Habilitamos seguridad
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme =
    CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme =
    CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie();

// Add services to the container.
string connectionString =
    builder.Configuration.GetConnectionString("SqlServer");
builder.Services.AddTransient<RepositoryDoctores>();
builder.Services.AddDbContext<DoctoresContext>
    (options => options.UseSqlServer(connectionString));

// Personalizamos rutas
builder.Services.AddControllersWithViews
    (options => options.EnableEndpointRouting = false)
    .AddSessionStateTempDataProvider();

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

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.UseMvc(routes =>
{
    routes.MapRoute(
    name: "default",
    template: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
