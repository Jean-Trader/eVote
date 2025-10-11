using Microsoft.AspNetCore.Mvc;

namespace eVote.Controllers
{
    public class PoliticalController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
