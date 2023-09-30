using QuestionExplorer.Entities;
using QuestionExplorer.Models;

namespace QuestionExplorer.Admins.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        Task<Admin> GetAdminByEmailAsync(string email);
        Task CreateAdminAsync(UserModel adminUser);
    }
}
