using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using pruebaCaso1.Data;
using pruebaCaso1.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ClaseEfContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ClaseEFDb")
    ));

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ClaseEfContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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



// Crear los roles automáticamente al iniciar la aplicación
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider
        .GetRequiredService<RoleManager<IdentityRole>>();

    string[] roles = { "Administrador", "Cliente" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(
                new IdentityRole(role)
            );
        }
    }
}

app.Run();

