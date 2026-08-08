using System.ComponentModel.DataAnnotations;

namespace pruebaCaso1.ViewModels;

// Modelo utilizado para almacenar los datos del inicio de sesión.
public class LoginViewModel
{
    // Correo electrónico del usuario.
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    // Contraseña del usuario.
    [Required, DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    // Indica si el usuario desea mantener la sesión iniciada.
    public bool RememberMe { get; set; }
}