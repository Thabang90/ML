using QuestionExplorer.Entities;
using QuestionExplorer.Models;

namespace QuestionExplorer.Users.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> LoginAsync(User user);
        Task UpsertUserAsync(UserModel userModel);
        Task<List<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByEmail(string email);
        Task DeleteUserAsync(User user);
        Task UpdateUserAsync(User user);
    }
}
