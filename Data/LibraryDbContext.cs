using Microsoft.EntityFrameworkCore;
using Library_BDwAI.Models;
namespace Library_BDwAI.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Loan> Loans { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Create initial data
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Fantasy" },
                new Genre { Id = 2, Name = "Science Fiction" },
                new Genre { Id = 3, Name = "Romans" },
                new Genre { Id = 4, Name = "Biografia" },
                new Genre { Id = 5, Name = "Naukowy" },
                new Genre { Id = 6, Name = "Kryminał" }
            );
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, ISBN="412351", Title = "Władca Pierścieni", Author = "J.R.R. Tolkien", GenreId = 1, PublishedYear = 1954, CopiesAvailable = 5 },
                new Book { Id = 2, ISBN = "465351", Title = "1984", Author = "George Orwell", GenreId = 2, PublishedYear = 1949, CopiesAvailable = 3 },
                new Book { Id = 3, ISBN = "413142", Title = "Duma i uprzedzenie", Author = "Jane Austen", GenreId = 3, PublishedYear = 1813, CopiesAvailable = 3 },
                new Book { Id = 4, ISBN = "422355", Title = "Steve Jobs", Author = "Walter Isaacson", GenreId = 4, PublishedYear = 2011, CopiesAvailable = 4 }
            );
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, FirstName = "Admin", LastName = "Admin", Email = "a@example.com", Password = "AdminPass123", IsAdmin = true },
                new User { Id = 2, FirstName = "John", LastName = "Smith", Email = "john24@example.com", Password = "UserPass456", IsAdmin = false }
            );

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
