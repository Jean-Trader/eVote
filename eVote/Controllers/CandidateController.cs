using AutoMapper;
using eVote.Core.Application.DTOs.Candidate;
using eVote.Core.Application.DTOs.Citizen;
using eVote.Core.Application.DTOs.Party;
using eVote.Core.Application.Interfaces;
using eVote.Core.Application.Services;
using eVote.Core.Application.ViewModels.Candidate;
using eVote.Core.Application.ViewModels.Citizen;
using eVote.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace eVote.Controllers
{
    [Controller]
    public class CandidateController : Controller
    {
        IMapper _mapper;
        ISessions _sessions;
        ICandidateService _candidateService;
        IValidateElection _validateElection;
        public CandidateController(IMapper mapper, ISessions sessions, ICandidateService candidateService, IElectionService electionService, IValidateElection validateElection)
        {
            _mapper = mapper;
            _sessions = sessions;
            _candidateService = candidateService;
            _validateElection = validateElection;
        }
        public async Task<IActionResult> Index()
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var candidates = await _candidateService.GetAllAsync();
            var candidateViewModels = _mapper.Map<List<CandidateViewModel>>(candidates);

            return View(candidateViewModels);
        }

        public IActionResult Create()
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
          
            return View("Save", new CreateCandidateViewModel { FirstName = "", LastName = "", PartyId = 0, Description = "", Status = true });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCandidateViewModel vm)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
          
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Error al agregar candidato. Por favor, verifique los datos ingresados.";
                return View("Save", vm);
            }
            var validate = _validateElection.ValidateExistActiveElection();
            if (validate)
            {
                ViewBag.ErrorMessage = "No se pueden agregar candidatos mientras hay una eleccion activa.";
                return RedirectToRoute(new { controller = "Candidate", action = "Index" });
            }

            try
            {
                var candidateDto = _mapper.Map<CandidateDto>(vm);

                var UpCandidate = await _candidateService.AddAsync(candidateDto);

                if (UpCandidate != null)
                {
                    candidateDto.Id = UpCandidate.Id;
                    candidateDto.Photo = UploadFile.Uploader(vm.Photo, candidateDto.Id, "Candidates");

                    await _candidateService.UpdateAsync(UpCandidate.Id, candidateDto);
                }

                return RedirectToRoute(new { controller = "Candidate", action = "Index" });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al agregar candidato: " + ex.Message;
                return RedirectToRoute(new { controller = "Candidate", action = "Index" });
            }

        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
         
            var candidateDto = await _candidateService.GetByIdAsync(id);
            var vm = _mapper.Map<CandidateViewModel>(candidateDto);
            if (vm == null)
            {
                ViewBag.ErrorMessage = "No se ha encontrado el candidato";
                return RedirectToRoute(new { controller = "Candidate", action = "Index" });
            }
            ViewBag.EditMode = true;
            return View("Save", vm);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(CreateCandidateViewModel vm)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
          
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Error al editar candidato. Por favor, verifique los datos ingresados.";
                ViewBag.EditMode = true;
                return View("Save", vm);
            }
            var validate = _validateElection.ValidateExistActiveElection();
            if (validate)
            {
                ViewBag.ErrorMessage = "No se pueden Editar candidatos mientras hay una eleccion activa.";
                return RedirectToRoute(new { controller = "Candidate", action = "Index" });
            }
            try
            {
                CandidateDto candidateDto = _mapper.Map<CandidateDto>(vm);
                if (vm.Photo != null)
                {
                    candidateDto.Photo = UploadFile.Uploader(vm.Photo, vm.Id, "Candidates");
                }
                await _candidateService.UpdateAsync(vm.Id, candidateDto);
                return RedirectToRoute(new { controller = "Candidate", action = "Index" });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al editar candidato: " + ex.Message;
                ViewBag.EditMode = true;
                return View("Save", vm);
            }
        }
        public async Task<IActionResult> ChangeStatus(CandidateViewModel vm)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
           
            var candidateDto = await _candidateService.GetByIdAsync(vm.Id);
            var candidate = _mapper.Map<CandidateViewModel>(candidateDto);

            if (candidate == null)
            {
                ViewBag.ErrorMessage = "No se ha encontrado el ciudadano";
                return RedirectToRoute(new { controller = "Candidate", action = "Index" });
            }
            ViewBag.EditMode = true;
            return View(candidate);
        }
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
         
            var validate = _validateElection.ValidateExistActiveElection();
            if (validate)
            {
                ViewBag.ErrorMessage = "No se puede cambiar el estado de los candidatos mientras hay una eleccion activa.";
                return RedirectToRoute(new { controller = "Candidate", action = "Index" });
            }
            try
            {
                var result = await _candidateService.ChangeStatusAsync(id);
                if (result == null)
                {
                    ViewBag.ErrorMessage = "No se ha encontrado al candidato";
                    return RedirectToRoute(new { controller = "Candidate", action = "Index" });
                }
                return RedirectToRoute(new { controller = "Candidate", action = "Index" });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al cambiar el estado del candidato: " + ex.Message;
                return RedirectToRoute(new { controller = "Candidate", action = "Index" });
            }
            
        }
    }
}
