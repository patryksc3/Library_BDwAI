using System.ComponentModel.DataAnnotations;

namespace Library_BDwAI.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nazwa gatunku jest wymagana.")]
        [Display(Name = "Nazwa gatunku")]
        public string Name { get; set; }

        public virtual ICollection<Book>? Books { get; set; }
    }
}
