using System.ComponentModel.DataAnnotations;

namespace PhunnyShop.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Nome")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Apelido")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Contacto")]
        public string Contact {  get; set; }

        [Required]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
