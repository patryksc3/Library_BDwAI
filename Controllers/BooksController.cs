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
        public async Task<IActionResult> DeleteBook(int id, int copiesToRemove)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            if (copiesToRemove <= 0 || copiesToRemove > book.CopiesAvailable)
            {
                TempData["Error"] = "Nieprawidłowa liczba egzemplarzy do usunięcia.";
                return RedirectToAction(nameof(BooksList));
            }

            book.CopiesAvailable -= copiesToRemove;

            if (book.CopiesAvailable <= 0)
            {
                _context.Books.Remove(book);
            }
            else
            {
                _context.Books.Update(book);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = $"Usunięto {copiesToRemove} egz. książki \"{book.Title}\".";

            return RedirectToAction(nameof(BooksList));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Genre)
                .Include(b => b.Loans)
                    .ThenInclude(l => l.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
    }
}       
