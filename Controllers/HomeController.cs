using Library_BDwAI.Models;
using Library_BDwAI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Library_BDwAI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult LoginPage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoginPage(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: sprawdzenie w bazie danych czy u¿ytkownik istnieje i logowanie
            }
            return View(model);
        }
        public IActionResult RegisterPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterPage(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User newUser = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Password = model.Password
                };

                //TODO: dodanie u¿ytkownika do bazy danych

                return RedirectToAction("LoginPage");
            }
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
