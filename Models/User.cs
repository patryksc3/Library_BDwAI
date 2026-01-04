using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Library_BDwAI.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Imię jest wymagane.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Nazwisko jest wymagane.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email jest wymagany.")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string? PhoneNumber { get; set; } = "";
        public bool IsAdmin { get; set; } = false;
        public virtual ICollection<Loan>? Loans { get; set; }
    }
}
