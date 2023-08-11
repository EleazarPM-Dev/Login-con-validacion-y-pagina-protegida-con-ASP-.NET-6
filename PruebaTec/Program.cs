using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PruebaTec.Models;
using PruebaTec.Models.SendEmail;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<PtitGroupContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PruebaContext"));
});


builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Registro/Index"; // Ruta de inicio de sesión
        options.LogoutPath = "/Registro/Index"; // Ruta de cierre de sesión
        options.AccessDeniedPath = "/Home/AccesoDenegado"; // Ruta de acceso denegado
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Tiempo de expiración de la cookie
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CampoBoolPolicy", policy =>
    {
        policy.RequireClaim("CampoBool", "true");
    });
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Registro}/{action=Index}/{id?}");

app.Run();
