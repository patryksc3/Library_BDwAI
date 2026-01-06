using Library_BDwAI.Data;
using Library_BDwAI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
        public IActionResult AddBook(Book model)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(model);
                _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book != null)
            {
                _context.Books.Remove(book);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(BooksList));
        }
    }
}
