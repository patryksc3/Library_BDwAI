using Library_BDwAI.Data;
using Library_BDwAI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Library_BDwAI.Controllers
{
    public class BooksController : Controller
    {
        private readonly LibraryDbContext _context;

        public BooksController(LibraryDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AddBook()
        {
            ViewBag.GenresList = new SelectList(_context.Genres, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBook(Book model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.GenresList = new SelectList(_context.Genres, "Id", "Name");
                return View(model);
            }

            _context.Books.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(BooksList));
        }

        public async Task<IActionResult> BooksList()
        {
            var books = _context.Books.Include(b => b.Genre);
            return View(await books.ToListAsync());
        }

        public async Task<IActionResult> EditBook(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var book = await _context.Books.FindAsync(id);
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", book.GenreId);
            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }
            var existingBook = await _context.Books.FindAsync(id);

            if (existingBook == null) {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                existingBook.CopiesAvailable = book.CopiesAvailable;
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.ISBN = book.ISBN;
                existingBook.PublishedYear = book.PublishedYear;
                existingBook.GenreId = book.GenreId;
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(BooksList));
            }
            return View(book);
        }

        public async Task<IActionResult> DeleteBook(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }
            _context.Books.Remove(book);


            await _context.SaveChangesAsync();
            TempData["Success"] = $"Książka {book.Title} została usunięta";

            return RedirectToAction(nameof(BooksList));
        }
    }
}       
