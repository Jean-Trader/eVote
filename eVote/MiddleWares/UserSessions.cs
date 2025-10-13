using eVote.Core.Application.Helpers;
using eVote.Core.Application.Interfaces;
using eVote.Core.Application.ViewModels.User;
namespace eVote.MiddleWares
{
    public class UserSessions : ISessions
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSessions(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserViewModel? GetUserSession()
        {
            UserViewModel? userViewModel = _httpContextAccessor.HttpContext?
                .Session.getSession<UserViewModel>("User");

            if (userViewModel == null)
            {
                return null;
            }

            return userViewModel;
        }
        

        public bool HasUser()
        {
            UserViewModel? userViewModel = _httpContextAccessor.HttpContext?.Session.getSession<UserViewModel>("User");

            if (userViewModel == null)
            {
                return false;
            }

            return true;
        }

        public bool IsAdmin()
        {
            UserViewModel? userViewModel = _httpContextAccessor.HttpContext?.Session.getSession<UserViewModel>("User");

            if (userViewModel == null)
            {
                return false;
            }

            return userViewModel.Role == "Admin";
        }
    }

    
}
