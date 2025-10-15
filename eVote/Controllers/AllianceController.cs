using AutoMapper;
using eVote.Core.Application.DTOs.Alliance;
using eVote.Core.Application.DTOs.AllianceRequest;
using eVote.Core.Application.Interfaces;
using eVote.Core.Application.ViewModels.Alliance;
using eVote.Core.Application.ViewModels.AllianceRequest;
using Microsoft.AspNetCore.Mvc;

namespace eVote.Controllers
{
    public class AllianceController : Controller
    {
        IMapper _mapper;
        ISessions _sessions;
        IAllianceService _allianceService;
        IAllianceRequestService _allianceRequestService;
        IPartyLeaderService _partyLeaderService;
        IPartyServices _partyService;
        IValidateElection _validateElection;

        public AllianceController(
            IMapper mapper,
            ISessions sessions,
            IAllianceService allianceService,
            IAllianceRequestService allianceRequestService,
            IPartyLeaderService partyLeaderService,
            IPartyServices partyService,
            IValidateElection validateElection)
        {
            _mapper = mapper;
            _sessions = sessions;
            _allianceService = allianceService;
            _allianceRequestService = allianceRequestService;
            _partyLeaderService = partyLeaderService;
            _partyService = partyService;
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
                return View();
            }

            var partyId = partyLeader.PartyId;

            // Solicitudes pendientes recibidas
            var pendingRequests = _allianceRequestService.GetAllWithDetails()
                .Where(r => r.ReceivingPartyId == partyId && r.Status == "Pendiente")
                .ToList();

            // Solicitudes enviadas
            var sentRequests = _allianceRequestService.GetAllWithDetails()
                .Where(r => r.RequestingPartyId == partyId)
                .ToList();

            // Alianzas activas
            var activeAlliances = _allianceService.GetAllWithDetails()
                .Where(a => (a.Party1Id == partyId || a.Party2Id == partyId) && a.Status == true)
                .ToList();

            ViewBag.PendingRequests = _mapper.Map<List<AllianceRequestViewModel>>(pendingRequests);
            ViewBag.SentRequests = _mapper.Map<List<AllianceRequestViewModel>>(sentRequests);
            ViewBag.ActiveAlliances = _mapper.Map<List<AllianceViewModel>>(activeAlliances);
            ViewBag.PartyId = partyId;

            return View();
        }

        public async Task<IActionResult> CreateRequest()
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
                return RedirectToRoute(new { controller = "Alliance", action = "Index" });
            }

            var partyId = partyLeader.PartyId;

            var allRequests = _allianceRequestService.GetAllWithDetails();
            var myRequests = allRequests.Where(r => r.RequestingPartyId == partyId || r.ReceivingPartyId == partyId).ToList();

            var allParties = await _partyService.GetAllAsync();
            var availableParties = allParties.Where(p =>
                p.Id != partyId &&
                p.Status == true &&
                !myRequests.Any(r =>
                    (r.RequestingPartyId == partyId && r.ReceivingPartyId == p.Id && r.Status == "Pendiente") ||
                    (r.ReceivingPartyId == partyId && r.RequestingPartyId == p.Id && r.Status == "Pendiente")
                )
            ).ToList();

            ViewBag.AvailableParties = availableParties;
            ViewBag.RequestingPartyId = partyId;

            return View("SaveRequest", new AllianceRequestViewModel
            {
                RequestingPartyId = partyId,
                ReceivingPartyId = 0,
                RequestDate = DateTime.Now,
                Status = "Pendiente"
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest(AllianceRequestViewModel vm)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Error al crear solicitud. Por favor, verifique los datos ingresados.";
                return View("SaveRequest", vm);
            }

            var validate = _validateElection.ValidateExistActiveElection();
            if (validate)
            {
                ViewBag.ErrorMessage = "No se pueden crear solicitudes mientras hay una elección activa.";
                return RedirectToRoute(new { controller = "Alliance", action = "Index" });
            }

            try
            {
                AllianceRequestDto requestDto = _mapper.Map<AllianceRequestDto>(vm);
                requestDto.RequestDate = DateTime.Now;
                requestDto.Status = "Pendiente";
                await _allianceRequestService.AddAsync(requestDto);
                return RedirectToRoute(new { controller = "Alliance", action = "Index" });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al crear solicitud: " + ex.Message;
                return RedirectToRoute(new { controller = "Alliance", action = "Index" });
            }
        }

        public async Task<IActionResult> AcceptRequest(int id)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            var requestDto = await _allianceRequestService.GetByIdAsync(id);
            var request = _mapper.Map<AllianceRequestViewModel>(requestDto);

            if (request == null)
            {
                ViewBag.ErrorMessage = "No se ha encontrado la solicitud";
                return RedirectToRoute(new { controller = "Alliance", action = "Index" });
            }

            ViewBag.Action = "Accept";
            return View("ConfirmRequest", request);
        }

        [HttpPost]
        public async Task<IActionResult> AcceptRequest(AllianceRequestViewModel vm)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var validate = _validateElection.ValidateExistActiveElection();
            if (validate)
            {
                ViewBag.ErrorMessage = "No se pueden aceptar solicitudes mientras hay una elección activa.";
                return RedirectToRoute(new { controller = "Alliance", action = "Index" });
            }

            try
            {
                var requestDto = await _allianceRequestService.GetByIdAsync(vm.Id);
                requestDto.Status = "Aceptada";
                await _allianceRequestService.UpdateAsync(vm.Id, requestDto);

                var allianceDto = new AllianceDto
                {
                    Id = 0,
                    Party1Id = requestDto.RequestingPartyId,
                    Party2Id = requestDto.ReceivingPartyId,
                    AcceptedDate = DateTime.Now,
                    Status = true
                };
                await _allianceService.AddAsync(allianceDto);

                return RedirectToRoute(new { controller = "Alliance", action = "Index" });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al aceptar solicitud: " + ex.Message;
                return RedirectToRoute(new { controller = "Alliance", action = "Index" });
            }
        }

        public async Task<IActionResult> RejectRequest(int id)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var requestDto = await _allianceRequestService.GetByIdAsync(id);
            var request = _mapper.Map<AllianceRequestViewModel>(requestDto);

            if (request == null)
            {
                ViewBag.ErrorMessage = "No se ha encontrado la solicitud";
                return RedirectToRoute(new { controller = "Alliance", action = "Index" });
            }

            ViewBag.Action = "Reject";
            return View("ConfirmRequest", request);
        }

        [HttpPost]
        public async Task<IActionResult> RejectRequest(AllianceRequestViewModel vm)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var validate = _validateElection.ValidateExistActiveElection();
            if (validate)
            {
                ViewBag.ErrorMessage = "No se pueden rechazar solicitudes mientras hay una elección activa.";
                return RedirectToRoute(new { controller = "Alliance", action = "Index" });
            }

            try
            {
                var requestDto = await _allianceRequestService.GetByIdAsync(vm.Id);
                requestDto.Status = "Rechazada";
                await _allianceRequestService.UpdateAsync(vm.Id, requestDto);

                return RedirectToRoute(new { controller = "Alliance", action = "Index" });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al rechazar solicitud: " + ex.Message;
                return RedirectToRoute(new { controller = "Alliance", action = "Index" });
            }
        }

        public async Task<IActionResult> DeleteRequest(int id)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }

            var requestDto = await _allianceRequestService.GetByIdAsync(id);
            var request = _mapper.Map<AllianceRequestViewModel>(requestDto);

            if (request == null)
            {
                ViewBag.ErrorMessage = "No se ha encontrado la solicitud";
                return RedirectToRoute(new { controller = "Alliance", action = "Index" });
            }

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRequest(AllianceRequestViewModel vm)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            var validate = _validateElection.ValidateExistActiveElection();
            if (validate)
            {
                ViewBag.ErrorMessage = "No se pueden eliminar solicitudes mientras hay una elección activa.";
                return RedirectToRoute(new { controller = "Alliance", action = "Index" });
            }

            try
            {
                await _allianceRequestService.DeleteAsync(vm.Id);
                return RedirectToRoute(new { controller = "Alliance", action = "Index" });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al eliminar solicitud: " + ex.Message;
                return RedirectToRoute(new { controller = "Alliance", action = "Index" });
            }
        }
    }
}
    

