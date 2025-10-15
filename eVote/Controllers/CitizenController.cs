using AutoMapper;
using eVote.Core.Application.DTOs.Citizen;
using eVote.Core.Application.Interfaces;
using eVote.Core.Application.Services;
using eVote.Core.Application.ViewModels.Citizen;
using eVote.Core.Application.ViewModels.ElectivePosition;
using Microsoft.AspNetCore.Mvc;

namespace eVote.Controllers
{
    [Controller]
    public class CitizenController : Controller
    {
        IMapper _mapper;
        ISessions _sessions;
        ICitizenServices _citizenService;
        IValidateElection _validateElection;
        public CitizenController(IMapper mapper, ISessions sessions, ICitizenServices citizenService, IElectionService electionService, IValidateElection validateElection)
        {
            _mapper = mapper;
            _sessions = sessions;
            _citizenService = citizenService;
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
            List<CitizenDto> citizens = await _citizenService.GetAllAsync();
            var citizenViewModels = _mapper.Map<List<CitizenViewModel>>(citizens);

            return View(citizenViewModels);
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
            return View("Save", new CitizenViewModel { FirstName = "", LastName = "", IdentificationNumber = "", Email = "", Status = true });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CitizenViewModel vm)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!_sessions.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenegated" });
            }
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Error al agregar ciudadano. Por favor, verifique los datos ingresados.";
                return View("Save", vm);
            }
            var validate = _validateElection.ValidateExistActiveElection();
            if (validate)
            {
                ViewBag.ErrorMessage = "No se pueden agregar ciudadanos mientras hay una eleccion activa.";
                return RedirectToRoute(new { controller = "Citizen", action = "Index" });
            }

             var citizens = await _citizenService.GetAllAsync();

            bool duplicate = citizens.Any(c => c.IdentificationNumber == vm.IdentificationNumber);
            if (duplicate) 
            {
                ViewBag.ErrorMessage = "Ya hay un ciudadano con esta identificación";
                return RedirectToRoute(new { controller = "Citizen", action = "Index" });
            }
            try 
            {
                CitizenDto citizenDto = _mapper.Map<CitizenDto>(vm);
                await _citizenService.AddAsync(citizenDto);
                return RedirectToRoute(new { controller = "Citizen", action = "Index" });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al agregar ciudadano: " + ex.Message;
                return RedirectToRoute(new { controller = "Citizen", action = "Index" });
            }
           
        }

        public IActionResult Edit(int id)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!_sessions.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenegated" });
            }

            var citizenDto = _citizenService.GetByIdAsync(id).Result;
            var vm = _mapper.Map<CitizenViewModel>(citizenDto);
            if (vm == null)
            {
                ViewBag.ErrorMessage = "No se ha encontrado el ciudadano";
                return RedirectToRoute(new { controller = "Citizen", action = "Index" });
            }
            ViewBag.EditMode = true;
            return View("Save", vm);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(CitizenViewModel vm)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!_sessions.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenegated" });
            }
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Error al editar ciudadano. Por favor, verifique los datos ingresados.";
                ViewBag.EditMode = true;
                return View("Save", vm);
            }
            var validate = _validateElection.ValidateExistActiveElection();
            if (validate)
            {
                ViewBag.ErrorMessage = "No se pueden Editar ciudadanos mientras hay una eleccion activa.";
                return RedirectToRoute(new { controller = "Citizen", action = "Index" });
            }
            try
            {
                CitizenDto citizenDto = _mapper.Map<CitizenDto>(vm);
                await _citizenService.UpdateAsync(vm.Id,citizenDto);
                return RedirectToRoute(new { controller = "Citizen", action = "Index" });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al editar ciudadano: " + ex.Message;
                ViewBag.EditMode = true;
                return View("Save", vm);
            }
        }
        public async Task<IActionResult> ChangeStatus(CitizenViewModel vm)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!_sessions.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenegated" });
            }
            var citizenDto = await _citizenService.GetByIdAsync(vm.Id);
            var citizen = _mapper.Map<CitizenViewModel>(citizenDto);

          if (citizen == null)
            {
                ViewBag.ErrorMessage = "No se ha encontrado el ciudadano";
                return RedirectToRoute(new { controller = "Citizen", action = "Index" });
            }  
            return View(citizen);
        }
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int id)
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
                ViewBag.ErrorMessage = "No se puede cambiar el estado de los ciudadanos mientras hay una eleccion activa.";
                return RedirectToRoute(new { controller = "Citizen", action = "Index" });
            }
            try
            {
                var result = await _citizenService.ChangeStatusAsync(id);
                if (result == null)
                {
                    ViewBag.ErrorMessage = "No se ha encontrado al ciudadano";
                    return RedirectToRoute(new { controller = "Citizen", action = "Index" });
                }
                return RedirectToRoute(new { controller = "Citizen", action = "Index" });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al desactivar ciudadano: " + ex.Message;
                return RedirectToRoute(new { controller = "Citizen", action = "Index" });
            }
        }
    }
}
