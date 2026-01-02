using System.ComponentModel.DataAnnotations;

namespace Library_BDwAI.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Imię jest wymagane")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email jest wymagany")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Telefon")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Numer telefonu musi mieć 9 cyfr")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Hasło musi mieć co najmniej 8 znaków, zawierać wielką literę, małą literę, cyfrę oraz znak specjalny.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Powtórz hasło")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasła nie są identyczne")]
        public string ConfirmPassword { get; set; }
    }
}
