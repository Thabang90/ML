using Microsoft.AspNetCore.Mvc;
using QuestionExplorer.Admins.Repositories.Interfaces;
using QuestionExplorer.Entities;

namespace QuestionExplorer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository adminRepository;

        public AdminController(IAdminRepository adminRepository)
        {
            this.adminRepository = adminRepository;
        }

        //[HttpPost("add")]
        //public async Task<IActionResult> CreateAdminAsync([FromBody] Admin adminObj)
        //{
        //    var admin = await this.adminRepository.GetAdminByEmailAsync(adminObj.Email);
        //    if (admin != null)
        //        return BadRequest(new
        //        {
        //            Message = "Admin already exists!"
        //        });

        //    await this.adminRepository.CreateAdminAsync(adminObj);
        //    return Ok(new
        //    {
        //        Message = "Admin has been added successfully!"
        //    });
        //}
    }
}
