using AutoMapper;
using eVote.Core.Application.DTOs.Party;
using eVote.Core.Application.Interfaces;
using eVote.Core.Application.ViewModels.Party;
using Microsoft.AspNetCore.Mvc;
using eVote.Helpers;


namespace eVote.Controllers
{
    [Controller]
    public class PartyController : Controller
    {
        IMapper _mapper;
        ISessions _sessions;
        IPartyServices _partyService;
        IValidateElection _validateElection;
        public PartyController(IMapper mapper, ISessions sessions, IPartyServices partyService,
               IElectionService election, IValidateElection validateElection)
        {
            _mapper = mapper;
            _sessions = sessions;
            _partyService = partyService;
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

            var parties = await _partyService.GetAllAsync();
            var partyViewModels = _mapper.Map<List<UpPartyViewModel>>(parties);
            return View(partyViewModels);
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
            return View("Save", new PartyViewModel { Id = 0, Name = "", Acronym = "", Description = "", Status = true });
        }
        [HttpPost]
        public async Task<IActionResult> Create(PartyViewModel vm)
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
                ViewBag.ErrorMessage = "Datos Invalidos, intentelo denuevo";
                return View("Save", vm);
            }

            var validate = _validateElection.ValidateExistActiveElection();
            if (validate)
            {
                ViewBag.ErrorMessage = "No se puede crear partidos electivas mientras hay una eleccion activa.";
                return RedirectToRoute(new { controller = "Party", action = "Index" });
            }

            try
            {   
                var partyDto = _mapper.Map<PartyDto>(vm);

                var newParty = await _partyService.AddAsync(partyDto);

                if (newParty != null) 
                {
                    partyDto.Id = newParty.Id;
                    partyDto.Logo = UploadFile.Uploader(vm.LogoFile, partyDto.Id, "Parties");

                    await _partyService.UpdateAsync(partyDto.Id, partyDto);
                }

                return RedirectToRoute(new { controller = "Party", action = "Index" });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al crear partido", ex.Message);
                return View("Save", vm);
            }
        }

        public async Task<IActionResult> Edit(int Id)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!_sessions.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenegated" });
            }

            var partyDto = await _partyService.GetByIdAsync(Id);
            if (partyDto == null)
            {
                return NotFound();
            }
            var partyViewModel = _mapper.Map<PartyViewModel>(partyDto);
            ViewBag.EditMode = true;
            return View("Save", partyViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PartyViewModel vm)
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
                ViewBag.ErrorMessage = "Datos Invalidos, intentelo denuevo";
                return View("Save", vm);
            }
            var validate = _validateElection.ValidateExistActiveElection();
            if (validate)
            {
                ViewBag.ErrorMessage = "No se puede editar partidos mientras hay una eleccion activa.";
                return RedirectToRoute(new { controller = "Party", action = "Index" });
            }
            try
            {
                var newParty = await _partyService.GetByIdAsync(vm.Id);

                string? currentImagePath = "";

                if (newParty != null)
                {
                    currentImagePath = newParty.Logo;
                    newParty.Logo = UploadFile.Uploader(vm.LogoFile, newParty.Id, "Parties", true, currentImagePath)!;
                    newParty.Name = vm.Name;
                    newParty.Status = vm.Status;
                    newParty.Acronym = vm.Acronym;
                    newParty.Description = vm.Description;
                    await _partyService.UpdateAsync(newParty.Id, newParty);
                    return RedirectToRoute(new { controller = "Party", action = "Index" });
                }
                else 
                {
                    ViewBag.ErrorMessage = "No se encontró al partido";
                    return RedirectToRoute(new { controller = "Party", action = "Index" });
                }
               
               
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al editar partido", ex.Message);
                ViewBag.EditMode = true;
                return View("Save", vm);
            }
        }
       public async Task<IActionResult>ChangeStatus(int id)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!_sessions.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenegated" });
            }
            var partyDto = await _partyService.GetByIdAsync(id);
            var vm = _mapper.Map<PartyViewModel>(partyDto);


            if (partyDto == null)
            {
                ViewBag.ErrorMessage = "No se ha encontrado el partido";
                return RedirectToRoute(new { controller = "Party", action = "Index" });
            }
            
            return View(vm);
       }
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(PartyViewModel vm)
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
                ViewBag.ErrorMessage = "No se puede cambiar el estado de partidos mientras hay una eleccion activa.";
                return RedirectToRoute(new { controller = "Party", action = "Index" });
            }
            try
            {
               var result = await _partyService.ChangeStatusAsync(vm.Id);
                if (result == null)
                {
                    ViewBag.ErrorMessage = "No se ha encontrado el partido";
                    return RedirectToRoute(new { controller = "Party", action = "Index" });
                }
                return RedirectToRoute(new { controller = "Party", action = "Index" });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error al cambiar estado del partido", ex.Message);
                return View("Save", vm);

            }
        }
    }

    
}
