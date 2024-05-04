using System.ComponentModel.DataAnnotations;

namespace Shared.Auth
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "A megadott e-mail cím nem megfelelő.")]
        [Display(Name = "E-mail address")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "A jelszónak legalább {2} és legfeljebb {1} karakter hosszúnak kell lennie.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "A jelszó és az ellenőrző jelszó nem egyezik meg.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "The username consists a maximum of {1} characters")]
        [Display(Name = "Username")]
        public string UserName { get; set; }
    }
}
