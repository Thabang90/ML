using Microsoft.EntityFrameworkCore;
using QuestionExplorer.Admins.Repositories.Interfaces;
using QuestionExplorer.Context;
using QuestionExplorer.Entities;
using QuestionExplorer.Helpers;
using QuestionExplorer.Models;
using QuestionExplorer.Users.Repositories.Interfaces;
using QuestionExplorer.UtilityService.Interfaces;

namespace QuestionExplorer.Users.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext appContext;
        private readonly IEmailService emailService;
        private readonly IConfiguration config;
        private readonly IAdminRepository adminRepository;

        public UserRepository(AppDbContext appContext, IEmailService emailService, IConfiguration config, IAdminRepository adminRepository)
        {
            this.appContext = appContext;
            this.emailService = emailService;
            this.config = config;
            this.adminRepository = adminRepository;
        }

        public async Task<User> LoginAsync(User user)
        {
            var result = await appContext.Users.
                FirstOrDefaultAsync(u => u.Email == user.Email);

            return result;
        }

        public async Task UpsertUserAsync(UserModel userModel)
        {
            var result = await this.appContext.Users.FirstOrDefaultAsync(u => u.Id == userModel.Id || u.Email == userModel.Email || u.Username == userModel.Username);
            var admin = await this.appContext.Admins.FirstOrDefaultAsync(a => a.Email == userModel.Email);
            if (result == null)
            {
                var userPassword = PasswordGenerator.GenerateRandomPassword(this.config["PasswordChars"]);
                var user = new User()
                {
                    Email = userModel.Email,
                    Username = userModel.Username,
                    AdminId = admin != null ? admin.Id : null,
                    Password = PasswordHasher.HashPassword(userPassword),
                };

                await this.appContext.CreateAsync(user);

                var emailModel = new EmailModel(user.Email, "Login credentials", EmailBody.BuildNewUserPasswordEmailBody(user.Email, userPassword));
                this.emailService.SendEmail(emailModel);
            }
            else
            {
                result.Username = userModel.Username;
                result.Email = userModel.Email;
                await this.appContext.UpdateAsync(result);
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            await this.appContext.UpdateAsync(user);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            var result = await this.appContext.Users.ToListAsync();
            return result;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await this.appContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await this.appContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        } 

        public async Task DeleteUserAsync(User user)
        {
            await this.appContext.DeleteAsync(user);
        }
    }
}
