using eVote.Core.Application.DTOs.User;

namespace eVote.Core.Application.Interfaces
{
    public interface IUserService 
    {
        Task<UserDto?> AddAsync(CreateUserDto dto);
        Task<UserDto?> UpdateAsync(CreateUserDto dto);
        Task<bool> DeleteAsync(int id);
        Task<List<UserDto>> GetAll();
        Task<UserDto?> GetById(int id);
        Task<UserDto?> LoginAsync(LoginDto dto);
        void DefaultUser();
    }
}
