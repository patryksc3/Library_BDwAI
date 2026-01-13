using Library_BDwAI.Data;
using Library_BDwAI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Library_BDwAI.Controllers
{
    public class LoansController : Controller
    {
        private readonly LibraryDbContext _context;

        public LoansController(LibraryDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        // GET: Loans/AddLoan
        public async Task<IActionResult> AddLoan()
        {
            ViewBag.UsersList = new SelectList(
                await _context.Users.Where(u => !u.IsAdmin).ToListAsync(),
                "Id",
                "Email"
            );

            ViewBag.BooksList = new SelectList(
                await _context.Books.Where(b => b.CopiesAvailable > 0).ToListAsync(),
                "Id",
                "Title"
            );

            return View();
        }

        // POST: Loans/AddLoan
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLoan(int userId, int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book == null || book.CopiesAvailable <= 0)
            {
                TempData["Error"] = "Wybrana książka nie jest dostępna.";
                return RedirectToAction(nameof(AddLoan));
            }

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                TempData["Error"] = "Wybrany użytkownik nie istnieje.";
                return RedirectToAction(nameof(AddLoan));
            }

            var loan = new Loan
            {
                UserId = userId,
                BookId = bookId,
                LoanDate = DateTime.Now,
                ReturnDate = null
            };

            book.CopiesAvailable--;

            _context.Loans.Add(loan);
            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Wypożyczenie książki \"{book.Title}\" dla {user.Email} zostało zarejestrowane.";
            return RedirectToAction("Index", "Home");
        }

        // GET: Loans/Borrow/5 - dla zalogowanego użytkownika
        public async Task<IActionResult> Borrow(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "Nie wybrano książki.";
                return RedirectToAction("BooksList", "Books");
            }

            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["Error"] = "Musisz być zalogowany, aby wypożyczyć książkę.";
                return RedirectToAction("LoginPage", "Home");
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null || book.CopiesAvailable <= 0)
            {
                TempData["Error"] = "Wybrana książka nie jest dostępna.";
                return RedirectToAction("BooksList", "Books");
            }

            var loan = new Loan
            {
                UserId = userId.Value,
                BookId = book.Id,
                LoanDate = DateTime.Now,
                ReturnDate = null
            };

            book.CopiesAvailable--;

            _context.Loans.Add(loan);
            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Wypożyczono książkę \"{book.Title}\".";
            return RedirectToAction(nameof(MyLoans));
        }

        // GET: Loans/FinishLoan
        public async Task<IActionResult> FinishLoan()
        {
            var activeLoans = await _context.Loans
                .Include(l => l.Book)
                .Include(l => l.User)
                .Where(l => l.ReturnDate == null)
                .ToListAsync();

            return View(activeLoans);
        }

        // POST: Loans/FinishLoan
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinishLoan(int loanId)
        {
            var loan = await _context.Loans
                .Include(l => l.Book)
                .FirstOrDefaultAsync(l => l.Id == loanId);

            if (loan == null || loan.ReturnDate != null)
            {
                TempData["Error"] = "Wypożyczenie nie istnieje lub zostało już zwrócone.";
                return RedirectToAction(nameof(FinishLoan));
            }

            loan.ReturnDate = DateTime.Now;
            if (loan.Book != null)
            {
                loan.Book.CopiesAvailable++;
                _context.Books.Update(loan.Book);
            }

            _context.Loans.Update(loan);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"Zwrot książki \"{loan.Book?.Title}\" został zarejestrowany.";
            return RedirectToAction(nameof(FinishLoan));
        }

        // GET: Loans/MyLoans (dla zalogowanego użytkownika)
        public async Task<IActionResult> MyLoans()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("LoginPage", "Home");
            }

            var loans = await _context.Loans
                .Include(l => l.Book)
                .ThenInclude(b => b.Genre)
                .Where(l => l.UserId == userId.Value)
                .OrderByDescending(l => l.LoanDate)
                .ToListAsync();

            return View(loans);
        }
    }
}
