using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_BDwAI.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tytuł jest wymagany.")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Autor jest wymagany.")]
        [Display(Name = "Autor")]
        public string Author { get; set; }
        [Required(ErrorMessage = "ISBN jest wymagany.")]
        public string ISBN { get; set; }
        [Display(Name = "Rok Wydania")]
        public int PublishedYear { get; set; }
        [Display(Name = "Gatunek")]
        public int GenreId { get; set; }
        [ForeignKey("GenreId")]
        public virtual Genre? Genre { get; set; }
        public int CopiesAvailable { get; set; }
        public virtual ICollection<Loan>? Loans { get; set; }


    }
}
