using app_ingenieria_ufinet.Data;
using app_ingenieria_ufinet.Repositories.Login;
using app_ingenieria_ufinet.Repositories.Parametrization;
using app_ingenieria_ufinet.Repositories.PI;
using app_ingenieria_ufinet.Repositories.User;
using app_ingenieria_ufinet.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using static app_ingenieria_ufinet.Repositories.Login.LoginRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Servicios y Repositorios
builder.Services.AddTransient<ILoginRepository, LoginRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IDatabaseUtils, DatabaseUtils>();
builder.Services.AddTransient<IParametrizationRepository, ParametrizationRepository>();
builder.Services.AddTransient<IPIRepository, PIRepository>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<IUserService, UserService>();

//Auth
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(50);
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
{
    config.AccessDeniedPath = "/Home/Error";
});

//Necesitamos indicar un provedor de almacenamiento para tempdata
builder.Services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();

builder.Services.AddControllersWithViews(options => options.EnableEndpointRouting = false).AddSessionStateTempDataProvider();


//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("ADMINISTRADORES", policy => policy.RequireRole("ADMIN"));
//});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}

app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/Home/Error";
        await next();
    }
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Login}");
//pattern: "{controller=Login}/{action=Login}/{id?}");

app.Run();
