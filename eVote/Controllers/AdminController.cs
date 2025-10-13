using AutoMapper;
using eVote.Core.Application.Interfaces;
using eVote.Core.Application.ViewModels.Election;
using Microsoft.AspNetCore.Mvc;
using eVote.Core.Application.Helpers;

namespace eVote.Controllers
{
    [Controller]
    public class AdminController : Controller
    {
        private IElectionService _electionService;
        private IMapper _mapper;
        ISessions _session;
        public AdminController(IElectionService electionService,IMapper map,ISessions session)
        {
            _electionService = electionService;
            _mapper = map;
            _session = session;
        }
        public IActionResult Index()
        {
            try
            {
                if (!_session.HasUser())
                {
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                if (!_session.IsAdmin())
                {
                    return RedirectToRoute(new { controller = "Login", action = "AccessDenegated" });
                }

               var Elections = _electionService.GetAllWithDetails();
               List<int> years = Elections.Select(e => e.ElectionDate.Year).Distinct().OrderBy(y => y).ToList();
               ViewBag.AvailableYears = years;
               ViewBag.SelectedYear = years.FirstOrDefault();
               return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return View(new List<ElectoralResumeViewModel>());
            }
        }
        [HttpPost]
        public IActionResult Index(int year)
        {
            try
            {
                if (!_session.HasUser())
                {
                    return RedirectToRoute(new { controller = "Login", action = "Index" });
                }
                if (!_session.IsAdmin())
                {
                    return RedirectToRoute(new { controller = "Login", action = "AccessDenegated" });
                }

                var model = _electionService.GetElectoralResumeByYear(year);
                var ModelVM = _mapper.Map<List<ElectoralResumeViewModel>>(model);
                return View("Index", ModelVM);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ha ocurrido un error";
                Console.WriteLine($"An error occurred: {ex.Message}");
                return View("Index", new List<ElectoralResumeViewModel>());
            }
        }
    }
}
