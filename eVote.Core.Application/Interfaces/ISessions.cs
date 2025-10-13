using eVote.Core.Application.ViewModels.User;


namespace eVote.Core.Application.Interfaces
{
    public interface ISessions
    {
        UserViewModel? GetUserSession();
        bool HasUser();
        bool IsAdmin();
    }
}
