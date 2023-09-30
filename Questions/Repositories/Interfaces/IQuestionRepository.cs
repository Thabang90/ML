using QuestionExplorer.Entities;
using QuestionExplorer.Models;

namespace QuestionExplorer.Questions.Repositories.Interfaces
{
    public interface IQuestionRepository
    {
        Task AddQuestionAsync(string question, string subject);
        Task UpdateQuestionAsync(QuestionModel questionModel);
        Task<Question> GetQuestionByIdAsync(int questionId);
        Task<List<Question>> GetListAsync();
        Task DeleteQuestionAsync(Question question);
    }
}
