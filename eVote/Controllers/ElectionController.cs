using AutoMapper;
using eVote.Core.Application.DTOs.Election;
using eVote.Core.Application.Interfaces;
using eVote.Core.Application.ViewModels.Election;
using Microsoft.AspNetCore.Mvc;

namespace eVote.Controllers
{
    public class ElectionController : Controller
    {
        private readonly IElectionService _electionService;
        private readonly IPartyServices _partyService;
        private readonly IElectivePositionService _electivePositionService;
        private readonly ICandidacyServices _candidacyService;
        private readonly IMapper _mapper;
        private readonly ISessions _sessions;
        private readonly IValidateElection _validateElection;

        public ElectionController(
            IElectionService electionService,
            IPartyServices partyService,
            IElectivePositionService electivePositionService,
            ICandidacyServices candidacyService,
            IMapper mapper,
            ISessions sessions,
            IValidateElection validateElection)
        {
            _electionService = electionService;
            _partyService = partyService;
            _electivePositionService = electivePositionService;
            _candidacyService = candidacyService;
            _mapper = mapper;
            _sessions = sessions;
            _validateElection = validateElection;
        }

        public IActionResult Index()
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!_sessions.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenegated" });
            }

            var elections = _electionService.GetAllWithDetails();
            var electionViewModels = _mapper.Map<List<ElectionViewModel>>(elections);

     
            electionViewModels = electionViewModels.OrderByDescending(e => e.Status == "Active" ? 1 : 0)
                .ThenByDescending(e => e.ElectionDate)
                .ToList();

            return View(electionViewModels);
        }

        public IActionResult Create()
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
                ViewBag.ErrorMessage = "Ya existe una elección activa. No se puede crear una nueva.";
                return RedirectToRoute(new { controller = "Election", action = "Index" });
            }

            return View("Save", new ElectionViewModel
            {
                Name = "",
                ElectionDate = DateTime.Now,
                Status = "Active",
                CreatedDate = DateTime.Now
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(ElectionViewModel vm)
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
                ViewBag.ErrorMessage = "Ya existe una elección activa. No se puede crear una nueva.";
                return RedirectToRoute(new { controller = "Election", action = "Index" });
            }

            ModelState.Remove(nameof(vm.Status));

            vm.Status = "Active";
            vm.CreatedDate = DateTime.Now;
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Datos inválidos, inténtelo de nuevo";
                return View("Save", vm);
            }

            try
            {
                var electivePositions = await _electivePositionService.GetAllAsync();
                var activePositions = electivePositions.Where(p => p.Status).ToList();

                if (activePositions.Count == 0)
                {
                    ViewBag.ErrorMessage = "No hay puestos electivos activos para realizar una elección.";
                    return View("Save", vm);
                }

                var parties = await _partyService.GetAllAsync();
                var activeParties = parties.Where(p => p.Status).ToList();

                if (activeParties.Count < 2)
                {
                    ViewBag.ErrorMessage = "No hay suficientes partidos políticos para realizar una elección.";
                    return View("Save", vm);
                }

                var candidacies = _candidacyService.GetAllWithDetails();
                var validationErrors = new List<string>();

                foreach (var party in activeParties)
                {
                    var partyCandidacies = candidacies.Where(c => c.PartyId == party.Id && 
                    c.Candidate != null && c.Candidate.Status).ToList();

                    var partyPositions = partyCandidacies.Select(c => c.ElectivePositionId).Distinct().ToList();

                    var missingPositions = activePositions.Where(p => !partyPositions.Contains(p.Id)).ToList();

                    if (missingPositions.Any())
                    {
                        var positionNames = string.Join(", ", missingPositions.Select(p => p.Name));
                        validationErrors.Add($"El partido político {party.Name} [{party.Acronym}]no tiene candidatos registrados para los siguientes puestos electivos: {positionNames}.");
                    }
                }

                if (validationErrors.Any())
                {
                    ViewBag.ErrorMessage = string.Join(" ", validationErrors);
                    return View("Save", vm);
                }

                vm.Status = "Active";
                vm.CreatedDate = DateTime.Now;

                ElectionDto dto = _mapper.Map<ElectionDto>(vm);
                await _electionService.AddAsync(dto);
                return RedirectToRoute(new { controller = "Election", action = "Index" });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al crear elección: " + ex.Message;
                return View("Save", vm);
            }
        }

        public async Task<IActionResult> Finalize(int id)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!_sessions.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenegated" });
            }

            var elections = await _electionService.GetAllAsync();
            var election = elections.FirstOrDefault(e => e.Id == id);

            if (election == null)
            {
                ViewBag.ErrorMessage = "No se ha encontrado la elección";
                return RedirectToRoute(new { controller = "Election", action = "Index" });
            }

            var vm = _mapper.Map<ElectionViewModel>(election);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Finalize(ElectionViewModel vm)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!_sessions.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenegated" });
            }

            try
            {
                var electionDto = await _electionService.GetByIdAsync(vm.Id);
                if (electionDto == null)
                {
                    ViewBag.ErrorMessage = "No se ha encontrado la elección";
                    return RedirectToRoute(new { controller = "Election", action = "Index" });
                }

                electionDto.Status = "Finalized";
                await _electionService.UpdateAsync(vm.Id, electionDto);

                return RedirectToRoute(new { controller = "Election", action = "Index" });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al finalizar elección: " + ex.Message;
                return RedirectToRoute(new { controller = "Election", action = "Index" });
            }
        }

        public async Task<IActionResult> Results(int id)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!_sessions.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenegated" });
            }

            var elections = await _electionService.GetAllAsync();
            var election = elections.FirstOrDefault(e => e.Id == id);

            if (election == null)
            {
                ViewBag.ErrorMessage = "No se ha encontrado la elección";
                return RedirectToRoute(new { controller = "Election", action = "Index" });
            }

            if (election.Status != "Finalized")
            {
                ViewBag.ErrorMessage = "Solo se pueden ver resultados de elecciones finalizadas";
                return RedirectToRoute(new { controller = "Election", action = "Index" });
            }

            var vm = _mapper.Map<ElectionViewModel>(election);

            return View(vm);

        }
    }
}
