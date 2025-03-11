using System.ComponentModel.DataAnnotations;

namespace JwtAuthApi.Models
{
    public class UpdateProfileModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑüÜ\s]+$",
            ErrorMessage = "El nombre de usuario solo puede contener letras, números, tildes y espacios.")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}