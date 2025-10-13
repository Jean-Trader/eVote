using AutoMapper;
using eVote.Core.Application.DTOs.ElectivePosition;
using eVote.Core.Application.Interfaces;
using eVote.Core.Application.ViewModels.ElectivePosition;
using Microsoft.AspNetCore.Mvc;

namespace eVote.Controllers
{
    [Controller]
    public class ElectivePositionController : Controller
    {
        IElectivePositionService _electivePositionService;
        ISessions _sessions;
        IMapper _mapper;
        IValidateElection _validateElection;

        public ElectivePositionController(IElectivePositionService electivePositionService, ISessions sessions,
            IMapper mapper, IValidateElection validateElection)
        {
            _electivePositionService = electivePositionService;
            _sessions = sessions;
            _mapper = mapper;
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

            List<ElectivePositionDto> electivePositionDto = await _electivePositionService.GetAllAsync();
            List<ElectivePositionViewModel> model = _mapper.Map<List<ElectivePositionViewModel>>(electivePositionDto);
            return View(model);
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

            return View("Save", new ElectivePositionViewModel { Description = "", Name = "", Status = true});
        }
        [HttpPost]
        public async Task<IActionResult> Create(ElectivePositionViewModel vm)
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
                ViewBag.ErrorMessage = "No se puede crear posiciones electivas mientras hay una eleccion activa.";
                return RedirectToRoute(new { controller = "ElectivePosition", action = "Index" });
            }

            try
            {
                ElectivePositionDto dto = _mapper.Map<ElectivePositionDto>(vm);
                await _electivePositionService.AddAsync(dto);
                return RedirectToRoute(new { controller = "ElectivePosition", action = "Index" });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ha ocurrido un error, intentelo denuevo";
                return View("Create", vm);
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

            ViewBag.EditMode = true;

            ElectivePositionDto? dto = await _electivePositionService.GetByIdAsync(Id);

            if (dto == null)
            {
                ViewBag.ErrorMessage = "No se ha encontrado el cargo electoral";
                return RedirectToRoute(new { controller = "ElectivePosition", action = "Index" });
            }
            var vm = _mapper.Map<ElectivePositionViewModel>(dto);
            return View("Save", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ElectivePositionViewModel vm)
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
                ViewBag.EditMode = true;
                return View("Save", vm);
            }
            var validate = _validateElection.ValidateExistActiveElection();
            if (validate)
            {
                ViewBag.ErrorMessage = "No se puede editar posiciones electivas mientras hay una eleccion activa.";
                return RedirectToRoute(new { controller = "ElectivePosition", action = "Index" });
            }
            try
            {
                ElectivePositionDto dto = _mapper.Map<ElectivePositionDto>(vm);
                await _electivePositionService.UpdateAsync(vm.Id, dto);
                return RedirectToRoute(new { controller = "ElectivePosition", action = "Index" });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ha ocurrido un error, intentelo denuevo";
                return View("Save", vm);
            }
        }



        public async Task<IActionResult> ChangeStatus(ElectivePositionViewModel vm)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!_sessions.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenegated" });
            }
            var electiveDto = await _electivePositionService.GetByIdAsync(vm.Id);
            var elective = _mapper.Map<ElectivePositionViewModel>(electiveDto);

            if (elective == null)
            {
                ViewBag.ErrorMessage = "No se ha encontrado el cargo electoral";
                return RedirectToRoute(new { controller = "ElectivePosition", action = "Index" });
            }
            return View(elective);
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
                ViewBag.ErrorMessage = "No se puede cambiar el estado de posiciones electivas mientras hay una elección activa.";
                return RedirectToRoute(new { controller = "ElectivePosition", action = "Index" });
            }
            try
            {
               var result = await _electivePositionService.ChangeStatusAsync(id);
                if (result == null) 
                { 
                 ViewBag.ErrorMessage = "No se ha encontrado el cargo electoral";
                 return RedirectToRoute(new { controller = "ElectivePosition", action = "Index" });
                }
                return RedirectToRoute(new { controller = "ElectivePosition", action = "Index" });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Ha ocurrido un error, intentelo denuevo";
                return RedirectToRoute(new { controller = "ElectivePosition", action = "Index" });
            }
        }
    }
}
