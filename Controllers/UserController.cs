using Microsoft.AspNetCore.Mvc;
using QuestionExplorer.Admins.Repositories.Interfaces;
using QuestionExplorer.Entities;
using QuestionExplorer.Helpers;
using QuestionExplorer.Models;
using QuestionExplorer.Users.Repositories.Interfaces;
using QuestionExplorer.UtilityService.Interfaces;

namespace QuestionExplorer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IAdminRepository adminRepository;
        private readonly IConfiguration config;
        private readonly IEmailService emailService;
        public UserController(IUserRepository userRepository, IAdminRepository adminRepository, IConfiguration config, IEmailService emailService)
        {
            this.userRepository = userRepository;
            this.adminRepository = adminRepository;
            this.config = config;
            this.emailService = emailService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> LoginAsync([FromBody] User userObj)
        {
            var user = await this.userRepository.LoginAsync(userObj);
            if (user is null)
                return NotFound(new { Message = "User Not found!" });

            if (!PasswordHasher.VerifyPassword(userObj.Password, user.Password))
                return BadRequest(new { Message = "Password is incorrect!" });


            var userModel = new UserModel()
            {
                Email = user.Email,
                Username = user.Username,
                IsAdmin = (await this.adminRepository.GetAdminByEmailAsync(user.Email)) != null
            };

            var response = new
            {
                UserModel = userModel,
                Message = "Login Successful, Redirecting to dashboard!"
            };

            return Ok(response);
        }

        [HttpPost("upsert")]
        public async Task<IActionResult> UpsertAsync([FromBody] UserModel user)
        {
            
            if (user.IsAdmin)
            {
                await this.adminRepository.CreateAdminAsync(user);
            }

            await this.userRepository.UpsertUserAsync(user);

            return Ok(new
            {
                Message = "User successsfully upserted!"
            });
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsersAsync()
        {
            var users = await this.userRepository.GetUsersAsync();
            return Ok(users);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserByIdAsync(int userId)
        {
            var user = await this.userRepository.GetUserByIdAsync(userId);

            if (user is null)
                return NotFound(new { Message = "User Not found!" });

            return Ok(user);
        }

        [HttpPost("send-reset-email/{email}")]
        public async Task<IActionResult> SendEmailAsync(string email)
        {
            var user = await this.userRepository.GetUserByEmail(email);

            if (user is null)
                return NotFound(new { Message = "Email doesnt exist!" });

            string from = this.config["EmailSettings:From"];
            var emailModel = new EmailModel(email, "Reset Password", EmailBody.BuildForgotPasswordEmailBody(email));
            this.emailService.SendEmail(emailModel);
            return Ok(new
            {
                Message = "Email has been sent with a link to reset your password"
            });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordModel resetPasswordModel)
        {
            var user = await this.userRepository.GetUserByEmail(resetPasswordModel.Email);

            if (user is null)
                return NotFound(new { Message = "Email not found, Contact Admin!" });

            user.Password = PasswordHasher.HashPassword(resetPasswordModel.Password);
            await this.userRepository.UpdateUserAsync(user);
            return Ok(new
            {
                Message = "Password has been updated successfully!"
            });
        }

        [HttpPost("delete/{userId}")]
        public async Task<IActionResult> DeleteUserAsync(int userId)
        {
            var user = await this.userRepository.GetUserByIdAsync(userId);
            if(user is null)
                return NotFound(new { Message = "User Not found!" });

            await this.userRepository.DeleteUserAsync(user);
            return Ok(new
            {
                Message = "User has been successfully deleted!"
            });
        }
    }
}
