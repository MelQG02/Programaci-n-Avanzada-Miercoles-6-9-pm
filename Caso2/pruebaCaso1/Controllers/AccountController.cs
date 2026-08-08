using pruebaCaso1.Models;
using pruebaCaso1.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace pruebaCaso1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Crear usuario de Identity
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(
                user,
                model.Password
            );

            // Si ocurre algún error al crear el usuario
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(
                        string.Empty,
                        error.Description
                    );
                }

                return View(model);
            }

          
            // Validar que solamente se pueda seleccionar
            // uno de los dos roles permitidos.
            if (model.Rol != "Cliente" &&
                model.Rol != "Administrador")
            {
                await _userManager.DeleteAsync(user);

                ModelState.AddModelError(
                    string.Empty,
                    "El rol seleccionado no es válido."
                );

                return View(model);
            }

            // Asignar el rol elegido por el usuario
            await _userManager.AddToRoleAsync(
                user,
                model.Rol
            );

            // Iniciar sesión automáticamente
            await _signInManager.SignInAsync(
                user,
                isPersistent: false
            );

        

            if (model.Rol == "Administrador")
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }


        
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(
            LoginViewModel model,
            string? returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _signInManager.PasswordSignInAsync(
                model.Email,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: false
            );

            if (!result.Succeeded)
            {
                ModelState.AddModelError(
                    string.Empty,
                    "Correo o contraseña incorrectos."
                );

                return View(model);
            }

            // Si venía de una página protegida,
            // regresar a esa página.
            if (!string.IsNullOrEmpty(returnUrl) &&
                Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            // Obtener usuario
            var user = await _userManager.FindByEmailAsync(
                model.Email
            );

            // Administrador
            if (user != null &&
                await _userManager.IsInRoleAsync(
                    user,
                    "Administrador"))
            {
                return RedirectToAction("Index", "Home");
            }

            // Cliente
            return RedirectToAction("Index", "Home");
        }


     

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }


        

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

