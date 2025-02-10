using AutoMapper;
using TrainingApp.Application.Dtos.User;
using TrainingApp.Domain.Entities;
using TrainingApp.Infrastructure.Interfaces;
using TrainingApp.Shared.Constants;
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

        public async Task<ApiResponse<Guid>> AddUser(UserInsertDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            var hashedPassword = PasswordHelper.HashPassword(user.Username, user.Password);

            user.Password = hashedPassword;

            var result = await _userRepository.AddUserAsync(user);

            var success = !result.Equals(Guid.Empty);

            return ResponseHelper<Guid>.GetResponse(result, success, ResultMessages.InsertMessage, ResultMessages.FailMessage);
        }

        public async Task<ApiResponse<bool>> UpdateUser(UserUpdateDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            var result = await _userRepository.UpdateUserAsync(user);

            return ResponseHelper<bool>.GetResponse(result, ResultMessages.UpdatedMessage, ResultMessages.FailMessage);
        }

        public async Task<ApiResponse<bool>> DeleteUser(Guid userId)
        {
            var result = await _userRepository.DeleteUserAsync(userId);

            return ResponseHelper<bool>.GetResponse(result, ResultMessages.DeletedMessage, ResultMessages.FailMessage);
        }

        public async Task<ApiResponse<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();

            var userList = _mapper.Map<IEnumerable<UserDto>>(users);

            var success = userList.Any();

            return ResponseHelper<IEnumerable<UserDto>>.GetResponse(userList, success, ResultMessages.SuccessMessage, ResultMessages.FailMessage);
        }

        public async Task<ApiResponse<UserDto>> GetUserById(Guid userId)
        {
            var users = await _userRepository.GetUserByIdAsync(userId);

            var user = _mapper.Map<UserDto>(users);

            var success = user != null;

            return ResponseHelper<UserDto>.GetResponse(user, success, ResultMessages.SuccessMessage, ResultMessages.FailMessage);
        }
    }
}
