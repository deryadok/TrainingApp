using TrainingApp.Domain.Entities;

namespace TrainingApp.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<Guid> AddUserAsync(User user);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(Guid id);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(Guid id);
    }
}
