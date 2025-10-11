using eVote.Core.Application.Interfaces;
using eVote.Core.Domain.Entities;
using eVote.Core.Domain.Interfaces;
using eVote.Infrastructure.Shared;
using eVote.Core.Application.Helpers;
using eVote.Core.Application.DTOs.User;

namespace eVote.Core.Application.Services
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto?> AddAsync(CreateUserDto dto)
        {
            try
            {
                var user = new User
                {
                    Id = dto.Id,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    UserName = dto.UserName,
                    PasswordHash = EncryptionPassword.Sha256Hash(dto.Password),
                    Email = dto.Email,
                    Role = dto.Role,
                    Status = dto.Status
                };
                var result = await _userRepository.AddAsync(user);

                if (result == null) return null;

                return new UserDto
                {
                    Id = result.Id,
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    UserName = result.UserName,
                    Email = result.Email,
                    Role = result.Role,
                    Status = result.Status
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
           var existingUser = await _userRepository.GetByIdAsync(id);
           CommonException.NotFound(existingUser!, $"not found");
           await _userRepository.DeleteAsync(id);
           return true;
        }

        public async Task<List<UserDto>> GetAll()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(user => new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
                Status = user.Status
            }).ToList();
        }
        public async Task<UserDto?> GetById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            CommonException.NotFound(user!, $"not found");

            return new UserDto
            {
                Id = id,
                FirstName = user!.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
                Status = user.Status
            };
        }

        public async Task<UserDto?> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.LoginAsync(dto.UserName, EncryptionPassword.Sha256Hash(dto.Password));

            CommonException.NotFound(user!, $"not found");
            return new UserDto
            {
                Id = user!.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
                Status = user.Status
            };
        }

        public async Task<UserDto?> UpdateAsync(CreateUserDto dto)
        {
            var existingUser = await _userRepository.GetByIdAsync(dto.Id);
            CommonException.NotFound(existingUser!, $"not found");
            existingUser!.FirstName = dto.FirstName;
            existingUser.LastName = dto.LastName;
            existingUser.UserName = dto.UserName;
            existingUser.PasswordHash = EncryptionPassword.Sha256Hash(dto.Password);
            existingUser.Email = dto.Email;
            existingUser.Role = dto.Role;
            existingUser.Status = dto.Status;
            var result = await _userRepository.UpdateAsync(dto.Id, existingUser);
            return new UserDto
            {
                Id = result.Id,
                FirstName = result.FirstName,
                LastName = result.LastName,
                UserName = result.UserName,
                Email = result.Email,
                Role = result.Role,
                Status = result.Status
            };
        }
    }
}
