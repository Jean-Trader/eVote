using eVote.Core.Domain.Entities;


namespace eVote.Core.Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> LoginAsync(string userName,string Password);
    }
}
