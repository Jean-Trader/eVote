using eVote.Core.Application.Interfaces;
using eVote.Core.Domain.Entities;
using eVote.Core.Domain.Interfaces;
using eVote.Infrastructure.Shared;
using eVote.Core.Application.Helpers;
using eVote.Core.Application.DTOs.User;
using AutoMapper;

namespace eVote.Core.Application.Services
{
    public class UserService : IUserService
    {
        IUserRepository _userRepository;
        IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapp)
        {
            _userRepository = userRepository;
            _mapper = mapp;
        }

        public async Task<UserDto?> AddAsync(CreateUserDto dto)
        {
            try
            {
               var user = _mapper.Map<User>(dto);
                var result = await _userRepository.AddAsync(user);

                if (result == null) return null;

                return _mapper.Map<UserDto>(result);
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

        public async Task<List<UserDto>> GetAllAsync()
        {
              var users = await _userRepository.GetAllAsync();
              var usersDto = users.Select(user => _mapper.Map<UserDto>(user)).ToList();
              return usersDto;
        }
        public async Task<UserDto?> GetById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            CommonException.NotFound(user!, $"not found");

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto?> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.LoginAsync(dto.UserName, EncryptionPassword.Sha256Hash(dto.Password));
            if (user == null){
                Console.WriteLine($"Usuario no encontrado: {dto.UserName}");
                throw new EVoteExceptions("Usuario o contraseña incorrectos.");
            }
            if (!user.Status)
            {
                Console.WriteLine($"Usuario inactivo: {user.UserName}");
                throw new EVoteExceptions("El usuario está inactivo.");
            }
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto?> UpdateAsync(CreateUserDto dto)
        {
            var existingUser = await _userRepository.GetByIdAsync(dto.Id);
            CommonException.NotFound(existingUser!, $"not found");
            _mapper.Map(dto, existingUser);
            var result = await _userRepository.UpdateAsync(dto.Id, existingUser);
            return _mapper.Map<UserDto>(result);
        }

        public async Task<bool> ChangeStatusAsync(int id) 
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) 
            {
                return false;
            }

            if (user.Status == true) 
            { 
             user.Status = false;
            }
            else { user.Status = true; }
            return true;

        }
    }
}
