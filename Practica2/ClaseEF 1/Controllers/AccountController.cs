using ClaseEF.Data;
using ClaseEF.Models;
using ClaseEF.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClaseEF.Controllers;

// Controlador encargado del registro, inicio y cierre de sesión de los usuarios.
public class AccountController : Controller
{
    // Servicios utilizados para gestionar usuarios, autenticación y base de datos.
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ClaseEfContext _context;

    // Constructor que inicializa los servicios necesarios.
    public AccountController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, ClaseEfContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
    }

    // Muestra el formulario de registro.
    [HttpGet]
    public IActionResult Register() => View(new RegisterViewModel());

    // Procesa el registro de un nuevo usuario.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        // Crea la cuenta del usuario.
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        // Muestra los errores si el registro falla.
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return View(model);
        }

        // Asigna el rol de Cliente.
        await _userManager.AddToRoleAsync(user, "Cliente");

        // Guarda la información del paciente relacionada con el usuario.
        var paciente = new Paciente
        {
            NombreDeLaPersona = model.NombreDeLaPersona,
            Identificacion = model.Identificacion,
            Telefono = model.Telefono,
            Correo = model.Email,
            FechaNacimiento = model.FechaNacimiento,
            Direccion = model.Direccion,
            UserId = user.Id
        };

        _context.Pacientes.Add(paciente);
        await _context.SaveChangesAsync();

        // Inicia sesión automáticamente después del registro.
        await _signInManager.SignInAsync(user, isPersistent: false);

        return RedirectToAction("Index", "Servicios");
    }

    // Muestra el formulario de inicio de sesión.
    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View(new LoginViewModel());
    }

    // Valida las credenciales del usuario.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        if (!ModelState.IsValid) return View(model);

        var result = await _signInManager.PasswordSignInAsync(
            model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

        // Muestra un mensaje si las credenciales son incorrectas.
        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Correo o contraseña incorrectos.");
            return View(model);
        }

        // Regresa a la página solicitada si existe.
        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        // Redirige según el rol del usuario.
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user is not null && await _userManager.IsInRoleAsync(user, "Administrador"))
            return RedirectToAction("Index", "Home");

        return RedirectToAction("Index", "Citas");
    }

    // Cierra la sesión del usuario.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    // Muestra la vista de acceso denegado.
    public IActionResult AccessDenied() => View();
}