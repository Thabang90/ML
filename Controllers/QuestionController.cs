using Microsoft.AspNetCore.Mvc;
using QuestionExplorer.Models;
using QuestionExplorer.Questions.Repositories.Interfaces;

namespace QuestionExplorer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private IQuestionRepository questionRepository;

        public QuestionController(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddQuestionAsync([FromBody] QuestionModel questionModel)
        {
            await this.questionRepository.AddQuestionAsync(questionModel.Question, questionModel.Subject);
            return Ok(new
            {
                Message = "Question has been added successfully"
            });
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateQuestionAsync([FromBody] QuestionModel questionModel)
        {
            await this.questionRepository.UpdateQuestionAsync(questionModel);
            return Ok(new
            {
                Message = "Question has been updated successfully"
            });
        }

        [HttpGet("questions")]
        public async Task<IActionResult> GetQuestionListAsync()
        {
            var results = await this.questionRepository.GetListAsync();
            return Ok(results);
        }

        [HttpGet]
        public async Task<IActionResult> GetQuestionByIdAsync(int questionId)
        {
            var result = await this.questionRepository.GetQuestionByIdAsync(questionId);
            return Ok(result);
        }

        [HttpPost("delete/{questionId}")]
        public async Task<IActionResult> DeleteQuestionAsync(int questionId)
        {
            var question = await this.questionRepository.GetQuestionByIdAsync(questionId);
            if(question is null)
                return NotFound(new { Message = "Question Not found!" });
            await this.questionRepository.DeleteQuestionAsync(question);

            return Ok(new
            {
                Message = "Question has been deleted successfully"
            });

        }
    }
}
