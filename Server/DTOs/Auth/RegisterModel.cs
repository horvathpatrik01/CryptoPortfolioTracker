using System.ComponentModel.DataAnnotations;

namespace Server.DTOs.Auth
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "A megadott e-mail cím nem megfelelő.")]
        [Display(Name = "E-mail cím")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "A jelszónak legalább {2} és legfeljebb {1} karakter hosszúnak kell lennie.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Jelszó")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Ellenőrző jelszó")]
        [Compare("Password", ErrorMessage = "A jelszó és az ellenőrző jelszó nem egyezik meg.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Keresztnév")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Vezetéknév")]
        public string LastName { get; set; }

        [Required]
        [StringLength(9, ErrorMessage = "A telefonszám 9 számjegyből kell hogy álljon.", MinimumLength = 9)]
        [Display(Name = "Telefonszám")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "A telefonszám csak számjegyeket tartalmazhat.")]
        public string PhoneNumber { get; set; }
    }
}
