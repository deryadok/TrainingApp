using TrainingApp.Domain.Entities;

namespace TrainingApp.Infrastructure.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task RegisterUserAsync(User user);
    }
}
