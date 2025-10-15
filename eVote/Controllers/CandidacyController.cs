using AutoMapper;
using eVote.Core.Application.DTOs.Candidacy;
using eVote.Core.Application.Interfaces;
using eVote.Core.Application.ViewModels.Candidacy;
using Microsoft.AspNetCore.Mvc;

namespace eVote.Controllers
{
    public class CandidacyController : Controller
    {
        IMapper _mapper;
        ISessions _sessions;
        ICandidacyServices _candidacyService;
        ICandidateService _candidateService;
        IElectivePositionService _electivePositionService;
        IPartyLeaderService _partyLeaderService;
        IAllianceService _allianceService;
        IValidateElection _validateElection;

        public CandidacyController(IMapper mapper, ISessions sessions,ICandidacyServices candidacyService, ICandidateService candidateService,
            IElectivePositionService electivePositionService,IPartyLeaderService partyLeaderService,IAllianceService allianceService,IValidateElection validateElection)
        {
            _mapper = mapper;
            _sessions = sessions;
            _candidacyService = candidacyService;
            _candidateService = candidateService;
            _electivePositionService = electivePositionService;
            _partyLeaderService = partyLeaderService;
            _allianceService = allianceService;
            _validateElection = validateElection;
        }

        public IActionResult Index()
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var user = _sessions.GetUserSession();
            var partyLeader = _partyLeaderService.GetAllWithDetails().FirstOrDefault(p => p.UserId == user.Id);

            if (partyLeader == null)
            {
                ViewBag.ErrorMessage = "No tienes un partido político asignado.";
                return View(new List<CandidacyViewModel>());
            }

            var candidacies = _candidacyService.GetAllWithDetails()
                .Where(c => c.PartyId == partyLeader.PartyId).ToList();

            var candidacyViewModels = _mapper.Map<List<CandidacyViewModel>>(candidacies);

            return View(candidacyViewModels);
        }

        public async Task<IActionResult> Create()
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var user = _sessions.GetUserSession();
            var partyLeader = _partyLeaderService.GetAllWithDetails().FirstOrDefault(p => p.UserId == user.Id);

            if (partyLeader == null)
            {
                ViewBag.ErrorMessage = "No tienes un partido político asignado.";
                return RedirectToRoute(new { controller = "Candidacy", action = "Index" });
            }

            
            var allCandidates = await _candidateService.GetAllAsync();
            var myCandidates = allCandidates.Where(c => (c.PartyId == partyLeader.PartyId || c.PartyId == null) && c.Status == true).ToList();


            var alliances = await _allianceService.GetAllAsync();
            var myAlliances = alliances.Where(a =>
            (a.Party1Id == partyLeader.PartyId || a.Party2Id == partyLeader.PartyId) && a.Status == true).ToList();

          
            foreach (var alliance in myAlliances)
            {
                int alliedPartyId = alliance.Party1Id == partyLeader.PartyId ? alliance.Party2Id : alliance.Party1Id;
                var alliedCandidates = allCandidates.Where(c => c.PartyId == alliedPartyId && c.Status == true).ToList();
                myCandidates.AddRange(alliedCandidates);
            }

       
            var assignedCandidateIds = _candidacyService.GetAllWithDetails().Where(c => c.PartyId == partyLeader.PartyId)
                .Select(c => c.CandidateId)
                .ToList();

            var availableCandidates = myCandidates.Where(c => !assignedCandidateIds.Contains(c.Id)).ToList();

         
            var allPositions = await _electivePositionService.GetAllAsync();
            var assignedPositionIds = _candidacyService.GetAllWithDetails().Where(c => c.PartyId == partyLeader.PartyId)
                .Select(c => c.ElectivePositionId).ToList();

            var availablePositions = allPositions.Where(p => p.Status == true && !assignedPositionIds.Contains(p.Id))
                .ToList();

            ViewBag.Candidates = availableCandidates;
            ViewBag.Positions = availablePositions;
            ViewBag.PartyId = partyLeader.PartyId;

            return View("Save", new CandidacyViewModel{ CandidateId = 0, ElectivePositionId = 0, PartyId = partyLeader.PartyId, RegistrationDate = DateTime.Now});
        }

        [HttpPost]
        public async Task<IActionResult> Create(CandidacyViewModel vm)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Error al asignar candidato. Por favor, verifique los datos ingresados.";
                return View("Save", vm);
            }

            var validate = _validateElection.ValidateExistActiveElection();
            if (validate)
            {
                ViewBag.ErrorMessage = "No se pueden asignar candidatos mientras hay una elección activa.";
                return RedirectToRoute(new { controller = "Candidacy", action = "Index" });
            }

            try
            {
                CandidacyDto candidacyDto = _mapper.Map<CandidacyDto>(vm);
                candidacyDto.RegistrationDate = DateTime.Now;
                await _candidacyService.AddAsync(candidacyDto);
                return RedirectToRoute(new { controller = "Candidacy", action = "Index" });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al asignar candidato: " + ex.Message;
                return RedirectToRoute(new { controller = "Candidacy", action = "Index" });
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
    
            var candidacyDto = await _candidacyService.GetByIdAsync(id);
            var candidacy = _mapper.Map<CandidacyViewModel>(candidacyDto);

            if (candidacy == null)
            {
                ViewBag.ErrorMessage = "No se ha encontrado la asignación";
                return RedirectToRoute(new { controller = "Candidacy", action = "Index" });
            }

            return View(candidacy);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(CandidacyViewModel vm)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            var validate = _validateElection.ValidateExistActiveElection();
            if (validate)
            {
                ViewBag.ErrorMessage = "No se pueden eliminar asignaciones mientras hay una elección activa.";
                return RedirectToRoute(new { controller = "Candidacy", action = "Index" });
            }

            try
            {
                await _candidacyService.DeleteAsync(vm.Id);
                return RedirectToRoute(new { controller = "Candidacy", action = "Index" });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al eliminar asignación: " + ex.Message;
                return RedirectToRoute(new { controller = "Candidacy", action = "Index" });
            }
        }
    }
}
    
