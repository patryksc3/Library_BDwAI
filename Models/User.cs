using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Library_BDwAI.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Imię jest wymagane.")]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email jest wymagany.")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Telefon")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Numer telefonu musi zawierać 9 cyfr.")]
        public string? PhoneNumber { get; set; }
        public virtual ICollection<Loan>? Loans { get; set; }
    }
}
