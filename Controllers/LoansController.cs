using Microsoft.AspNetCore.Mvc;

namespace Library_BDwAI.Controllers
{
    public class LoansController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
