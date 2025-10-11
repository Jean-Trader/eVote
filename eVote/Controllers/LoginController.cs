using Microsoft.AspNetCore.Mvc;

namespace eVote.Controllers
{
    [Controller]
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
