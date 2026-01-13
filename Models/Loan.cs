using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_BDwAI.Models
{
    public class Loan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name = "Data wypożyczenia")]
        public DateTime LoanDate { get; set; }
        [Display(Name = "Data zwrotu")]
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; } = false;
        public int BookId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("BookId")]
        public virtual Book? Book { get; set; }
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
    }
}
