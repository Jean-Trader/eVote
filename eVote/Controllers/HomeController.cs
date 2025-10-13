using System.Diagnostics;
using eVote.Core.Application.Interfaces;
using eVote.Models;
using Microsoft.AspNetCore.Mvc;

namespace eVote.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDefaultUser _defaultUser;

        public HomeController(IDefaultUser defaultUser)
        {
            _defaultUser = defaultUser;
        }

        public async Task<IActionResult> Index()
        {
            await _defaultUser.DefaultEntity();
            return View();
        }
    }
}
