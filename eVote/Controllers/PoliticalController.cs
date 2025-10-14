using AutoMapper;
using eVote.Core.Application.Interfaces;
using eVote.Core.Application.ViewModels.Candidate;
using eVote.MiddleWares;
using Microsoft.AspNetCore.Mvc;

namespace eVote.Controllers
{
    [Controller]
    public class PoliticalController : Controller
    {
        IDashboard _politicalDashboard;
        IMapper _mapper;
        ISessions _userSession;
        public PoliticalController(IDashboard dashboard,IMapper mapper, ISessions userSession) 
        {
            _politicalDashboard = dashboard;
            _mapper = mapper;
            _userSession = userSession;

        }
        
        public async Task<IActionResult> Index(int id)
        {
            var user = _userSession.GetUserSession();

           
            if (user == null)
                return RedirectToRoute(new { controller = "Login", action = "Index" });

            
            var dashboard = await _politicalDashboard.GetDashboard(user.Id);
            var dashBoardVM = _mapper.Map<PoliticalHomeViewModel>(dashboard);

            return View(dashBoardVM);
        }
    }
}
