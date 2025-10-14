using AutoMapper;
using eVote.Core.Application.DTOs.PartyLeader;
using eVote.Core.Application.Interfaces;
using eVote.Core.Application.ViewModels.PartyLeader;
using Microsoft.AspNetCore.Mvc;

namespace eVote.Controllers
{
    public class PartyLeaderController : Controller
    {
        private readonly IPartyLeaderService _partyLeaderService;
        private readonly IUserService _userService;
        private readonly IPartyServices _partyService;
        private readonly IMapper _mapper;
        private readonly ISessions _sessions;
        private readonly IValidateElection _validateElection;

        public PartyLeaderController(
            IPartyLeaderService partyLeaderService,
            IUserService userService,
            IPartyServices partyService,
            IMapper mapper,
            ISessions sessions,
            IValidateElection validateElection)
        {
            _partyLeaderService = partyLeaderService;
            _userService = userService;
            _partyService = partyService;
            _mapper = mapper;
            _sessions = sessions;
            _validateElection = validateElection;
        }

        public async Task<IActionResult> Index()
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!_sessions.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenegated" });
            }

            var partyLeaders = await _partyLeaderService.GetAllAsync();

            var partyLeaderViewModels = _mapper.Map<List<PartyLeaderViewModel>>(partyLeaders);

            return View(partyLeaderViewModels);
        }

        public async Task<IActionResult> Create()
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!_sessions.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenegated" });
            }

            var validate = _validateElection.ValidateExistActiveElection();
            if (validate)
            {
                ViewBag.ErrorMessage = "No se pueden asignar dirigentes mientras hay una elección activa.";
                return RedirectToRoute(new { controller = "PartyLeader", action = "Index" });
            }

            var allUsers = await _userService.GetAllAsync();

            var politicalUsers = allUsers.Where(u => u.Role == "Political" && u.Status).ToList();

            var assignedLeaders = _partyLeaderService.GetAllWithDetails();
            var assignedUserIds = assignedLeaders.Select(pl => pl.UserId).ToList();

            var availableUsers = politicalUsers.Where(u => !assignedUserIds.Contains(u.Id)).ToList();

            var parties = await _partyService.GetAllAsync();
            var activeParties = parties.Where(p => p.Status).ToList();

            ViewBag.Users = availableUsers;
            ViewBag.Parties = activeParties;

            return View("Save", new PartyLeaderViewModel { UserId = 0, PartyId = 0, AssignmentDate = DateTime.Now });
        }

        [HttpPost]
        public async Task<IActionResult> Create(PartyLeaderViewModel vm)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!_sessions.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenegated" });
            }

            var validate = _validateElection.ValidateExistActiveElection();
            if (validate)
            {
                ViewBag.ErrorMessage = "No se pueden asignar dirigentes mientras hay una elección activa.";
                return RedirectToRoute(new { controller = "PartyLeader", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Datos inválidos, inténtelo de nuevo";

                var allUsers = await _userService.GetAllAsync();
                var politicalUsers = allUsers.Where(u => u.Role == "Political" && u.Status).ToList();
                var assignedLeaders = _partyLeaderService.GetAllWithDetails();
                var assignedUserIds = assignedLeaders.Select(pl => pl.UserId).ToList();
                var availableUsers = politicalUsers.Where(u => !assignedUserIds.Contains(u.Id)).ToList();
                var parties = await _partyService.GetAllAsync();
                var activeParties = parties.Where(p => p.Status).ToList();

                ViewBag.Users = availableUsers;
                ViewBag.Parties = activeParties;

                return View("Save", vm);
            }


            var existingLeaders = _partyLeaderService.GetAllWithDetails();
            if (existingLeaders.Any(pl => pl.UserId == vm.UserId))
            {
                ViewBag.ErrorMessage = "Este dirigente ya está relacionado con otro partido político.";

                var allUsers = await _userService.GetAllAsync();
                var politicalUsers = allUsers.Where(u => u.Role == "Political" && u.Status).ToList();
                var assignedLeaders = _partyLeaderService.GetAllWithDetails();
                var assignedUserIds = assignedLeaders.Select(pl => pl.UserId).ToList();
                var availableUsers = politicalUsers.Where(u => !assignedUserIds.Contains(u.Id)).ToList();
                var parties = await _partyService.GetAllAsync();
                var activeParties = parties.Where(p => p.Status).ToList();

                ViewBag.Users = availableUsers;
                ViewBag.Parties = activeParties;

                return View("Save", vm);
            }

            try
            {
                PartyLeaderDto dto = _mapper.Map<PartyLeaderDto>(vm);
                await _partyLeaderService.AddAsync(dto);
                return RedirectToRoute(new { controller = "PartyLeader", action = "Index" });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al asignar dirigente: " + ex.Message;
                return RedirectToRoute(new { controller = "PartyLeader", action = "Index" });
            }
        }

        public IActionResult Delete(int id)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!_sessions.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenegated" });
            }

            var partyLeaders = _partyLeaderService.GetAllWithDetails();
            var partyLeader = partyLeaders.FirstOrDefault(pl => pl.Id == id);

            if (partyLeader == null)
            {
                ViewBag.ErrorMessage = "No se ha encontrado la asignación";
                return RedirectToRoute(new { controller = "PartyLeader", action = "Index" });
            }

            var vm = _mapper.Map<PartyLeaderViewModel>(partyLeader);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(PartyLeaderViewModel vm)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!_sessions.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenegated" });
            }

            var validate = _validateElection.ValidateExistActiveElection();
            if (validate)
            {
                ViewBag.ErrorMessage = "No se pueden eliminar asignaciones mientras hay una elección activa.";
                return RedirectToRoute(new { controller = "PartyLeader", action = "Index" });
            }

            try
            {
                var result = await _partyLeaderService.DeleteAsync(vm.Id);
                if (!result)
                {
                    ViewBag.ErrorMessage = "No se ha encontrado la asignación";
                }
                return RedirectToRoute(new { controller = "PartyLeader", action = "Index" });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al eliminar asignación: " + ex.Message;
                return RedirectToRoute(new { controller = "PartyLeader", action = "Index" });
            }
        }
    }
}
