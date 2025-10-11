using Microsoft.AspNetCore.Mvc;

namespace eVote.Controllers
{
    [Controller]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
