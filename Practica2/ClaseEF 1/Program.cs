using ClaseEF.Data;
using ClaseEF.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Agrega el soporte para controladores y vistas (MVC).
builder.Services.AddControllersWithViews();

// Configura la conexión con la base de datos SQL Server.
builder.Services.AddDbContext<ClaseEfContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClaseEFDb")));

// Configura el sistema de usuarios y roles.
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Ajustes básicos de contraseña e inicio de sesión.
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ClaseEfContext>()
.AddDefaultTokenProviders();

// Define las rutas para iniciar sesión y acceso denegado.
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

var app = builder.Build();

// Aplica migraciones pendientes (crea tablas AspNet* y demás si no existen)
// y crea los roles y un usuario administrador si no existen.
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var db = services.GetRequiredService<ClaseEfContext>();
    db.Database.Migrate(); // <-- aplica migraciones pendientes

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roles = { "Administrador", "Cliente" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
    }

    string adminEmail = "admin@clinica.com";
    if (await userManager.FindByEmailAsync(adminEmail) is null)
    {
        var admin = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        await userManager.CreateAsync(admin, "Admin123!");
        await userManager.AddToRoleAsync(admin, "Administrador");
    }
}

// Configura el manejo de errores en producción.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Habilita HTTPS y el enrutamiento.
app.UseHttpsRedirection();
app.UseRouting();

// Activa la autenticación y autorización de usuarios.
app.UseAuthentication();
app.UseAuthorization();

// Permite servir archivos estáticos (CSS, JS, imágenes).
app.MapStaticAssets();

// Define la ruta principal de la aplicación.
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// Inicia la aplicación.
app.Run();