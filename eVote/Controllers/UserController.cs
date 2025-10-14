using AutoMapper;
using eVote.Core.Application.DTOs.Citizen;
using eVote.Core.Application.DTOs.User;
using eVote.Core.Application.Interfaces;
using eVote.Core.Application.Services;
using eVote.Core.Application.ViewModels.Citizen;
using eVote.Core.Application.ViewModels.Party;
using eVote.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Mvc;


namespace eVote.Controllers
{
    [Controller]
    public class UserController : Controller
    {
        IMapper _mapper;
        IUserService _userService;
        ISessions _sessions;
        IValidateElection _validateElection;
        public UserController(IMapper mapper, IUserService userService, ISessions sessions, IValidateElection validateElection)
        {
            _mapper = mapper;
            _userService = userService;
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

           List<UserDto> users = await _userService.GetAllAsync();

           var usersVM = _mapper.Map<List<UserViewModel>>(users);

            return View(usersVM);
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

            return View("Save",new CreateUserViewModel { FirstName = "",LastName = " ",Email = "",Password = "",
                ConfirmPassword = "", UserName = "",Role = 0,Status = true});

        }

        [HttpPost]
        public async Task<IActionResult>Create(CreateUserViewModel CVM) 
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!_sessions.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenegated" });
            }

            var electionActive = _validateElection.ValidateExistActiveElection();
            if (electionActive) 
            {
                ViewBag.ErrorMessage = "No se puede crear usuarios mientras haya una elección activa";
                return RedirectToRoute(new {controller = "User", action = "Index" });
            }

            if (!ModelState.IsValid) 
            {
                ViewBag.ErrorMessage = "Datos no validos, inténtelo de nuevo";
                return View("Save",CVM);
            }

            var users = await _userService.GetAllAsync();

            var ValidateUserName = users.Any(u => u.UserName == CVM.UserName);
            var ValidateEmail = users.Any(e => e.Email == CVM.Email);

            if (ValidateUserName) 
            {
              ViewBag.ErrorMessage = "El nombre de usuario no está disponible";
              return View("Save", CVM);
            }

            if (ValidateEmail)
            {
              ViewBag.ErrorMessage = "Ya existe una cuenta con este correo";
              return View("Save",CVM);
            }

            if (CVM.Password != CVM.ConfirmPassword) 
            {
               ViewBag.ErrorMessage = " Las contraseñas no coinciden, verifica la contraseña y vuelva  intentarlo";
               return View("Save",CVM);
            }

            CreateUserDto userDto = _mapper.Map<CreateUserDto>(CVM);
            await _userService.AddAsync(userDto);
            return RedirectToRoute(new {controller = "User", action = "Index" });
        }

        public async Task<IActionResult>Edit(int id) 
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!_sessions.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenegated" });
            }

            var createUserDto = await _userService.GetById(id);

            var vm = _mapper.Map<CreateUserViewModel>(createUserDto);
            if (vm == null)
            {
                ViewBag.ErrorMessage = "No se ha encontrado Usuario";
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            ViewBag.EditMode = true;
            return View("Save", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CreateUserViewModel vm)
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
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            try
            {
                CreateUserDto userDto = _mapper.Map<CreateUserDto>(vm);
                await _userService.UpdateAsync(userDto);
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al editar usuario: " + ex.Message;
                ViewBag.EditMode = true;
                return View("Save", vm);
            }
        }
        public async Task<IActionResult> ChangeStatus(UserViewModel vm)
        {
            if (!_sessions.HasUser())
            {
                return RedirectToRoute(new { controller = "Login", action = "Index" });
            }
            if (!_sessions.IsAdmin())
            {
                return RedirectToRoute(new { controller = "Login", action = "AccessDenegated" });
            }
            var UserDto = await _userService.GetById(vm.Id);
            var user = _mapper.Map<UserViewModel>(UserDto);

            if (user == null)
            {
                ViewBag.ErrorMessage = "No se ha encontrado el usuario";
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            return View(user);
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
                ViewBag.ErrorMessage = "No se puede cambiar el estado de los ciudadanos mientras hay una elección activa.";
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            try
            {
                var result = await _userService.ChangeStatusAsync(id);

                if (result == false)
                {
                    ViewBag.ErrorMessage = "No se ha encontrado al usuario";
                    return RedirectToRoute(new { controller = "User", action = "Index" });
                }
                
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error al cambiar estado del  usuario: " + ex.Message;
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
        }
    }
}
