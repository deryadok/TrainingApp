using AutoMapper;
using TrainingApp.Application.Dtos;
using TrainingApp.Domain.Entities;
using TrainingApp.Infrastructure.Interfaces;
using TrainingApp.Shared.Helpers;
using TrainingApp.Shared.Response;

namespace TrainingApp.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ApiResponse<Guid>> AddUser(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            var hashedPassword = PasswordHelper.HashPassword(user.Username, user.Password);

            user.Password = hashedPassword;

            var result = await _userRepository.AddUserAsync(user);

            return ResponseHelper<Guid>.GetResponse(result, true);
        }
    }
}
