using System.ComponentModel.DataAnnotations;

namespace pruebaCaso1.ViewModels;

// Modelo utilizado para almacenar los datos del formulario de registro.
public class RegisterViewModel
{
    // Correo electrónico del usuario.
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    // Contraseña del usuario.
    [Required, DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    // Nombre completo de la persona.
    [Required, Display(Name = "Nombre completo")]
    public string NombreDeLaPersona { get; set; } = string.Empty;

    // Número de identificación del usuario.
    [Required]
    public string Identificacion { get; set; } = string.Empty;

    // Número de teléfono de contacto.
    [Required]
    public string Telefono { get; set; } = string.Empty;

    // Fecha de nacimiento del usuario.
    [Required, DataType(DataType.Date), Display(Name = "Fecha de nacimiento")]
    public DateTime FechaNacimiento { get; set; } = DateTime.Today.AddYears(-18);

    [Required]
    public string Rol { get; set; } = "Cliente";

    // Dirección de residencia del usuario.
    [Required]
    public string Direccion { get; set; } = string.Empty;
}