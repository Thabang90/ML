using Microsoft.EntityFrameworkCore;
using QuestionExplorer.Admins.Repositories.Interfaces;
using QuestionExplorer.Context;
using QuestionExplorer.Entities;
using QuestionExplorer.Helpers;
using QuestionExplorer.Models;

namespace QuestionExplorer.Admins.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext appContext;
        private readonly IConfiguration config;

        public AdminRepository(AppDbContext appContext, IConfiguration config)
        {
            this.appContext = appContext;
            this.config = config;
        }

        public async Task<Admin> GetAdminByEmailAsync(string email)
        {
            var result = await this.appContext.Admins.FirstOrDefaultAsync(a => a.Email == email);
            return result;
        }

        public async Task CreateAdminAsync(UserModel adminUser)
        {
            var admin = await this.appContext.Admins.FirstOrDefaultAsync(a => a.Email == adminUser.Email);
            if(admin is null)
            {
                admin = new Admin()
                {
                    Email = adminUser.Email,
                    Username = adminUser.Username,
                    Password = PasswordHasher.HashPassword(PasswordGenerator.GenerateRandomPassword(this.config["PasswordChars"])),
                };

                await this.appContext.CreateAsync(admin);
            }
        }
    }
}
