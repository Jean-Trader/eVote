using AutoMapper;
using eVote.Core.Application.DTOs.User;
using eVote.Core.Application.Helpers;
using eVote.Core.Application.Interfaces;
using eVote.Core.Application.ViewModels.User;
using eVote.Core.Domain.Enums;
using eVote.MiddleWares;
using Microsoft.AspNetCore.Mvc;

namespace eVote.Controllers
{
    [Controller]
    public class LoginController : Controller
    {
        IUserService _userService;  
        IMapper _mapper;
        ISessions _userSession;
        public LoginController(IUserService user, IMapper mapper, ISessions userSession)
        {
            _userService = user;
            _mapper = mapper;
            _userSession = userSession;
        }
        public IActionResult Index()
        {
            UserViewModel? userSession = _userSession.GetUserSession();
            if (userSession != null)
            {
                return userSession.Role switch
                {
                    "Political" => RedirectToRoute(new { controller = "Political", action = "Index" }),
                    "Admin" => RedirectToRoute(new { controller = "Admin", action = "Index" }),
                    _ => RedirectToRoute(new { controller = "Login", action = "Index" }),
                };
            }
            return View(new LoginViewModel() { UserName = "", Password = "" });
        }
        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            if (_userSession.HasUser())
            {
                UserViewModel? userSession = _userSession.GetUserSession();
                if (userSession != null)
                {
                    return userSession.Role switch
                    {
                        "Political" => RedirectToRoute(new { controller = "Political", action = "Index" }),
                        "Admin" => RedirectToRoute(new { controller = "Admin", action = "Index" }),
                        _ => RedirectToRoute(new { controller = "Login", action = "Index" }),
                    };
                }
            }
            if (ModelState.IsValid)
            {
                try
                {                    
                    var loginDto = _mapper.Map<LoginDto>(model);
                    UserDto? user = _userService.LoginAsync(loginDto).Result;
                    var userVM = _mapper.Map<UserViewModel>(user);

                    if (user != null)
                    {
                        HttpContext.Session.setSession<UserViewModel>("User", userVM);

                        if (userVM.Role == "Admin")
                            return RedirectToRoute(new { Controller = "Admin", Action = "Index" });
                        else
                            return RedirectToRoute(new { Controller = "Political", Action = "Index" });
                    }
                    ModelState.AddModelError("UserValidation", "Error, los campos no son validos");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("UserValidation", $"Error: {ex.Message}");
                }
            }

            return View(model);
        }
      public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToRoute(new { controller = "Login", action = "Index" });
        }
    }  
  
}
