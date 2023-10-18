using Microsoft.EntityFrameworkCore;
using QuestionExplorer.Context;
using QuestionExplorer.Entities;
using QuestionExplorer.Models;
using QuestionExplorer.Questions.Repositories.Interfaces;

namespace QuestionExplorer.Questions.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly AppDbContext appContext;

        public QuestionRepository(AppDbContext appContext)
        {
            this.appContext = appContext;
        }

        public async Task AddQuestionAsync(string question, string subject)
        {
            var questionToAdd = new Question()
            {
                PromptQuestion = question,
                Subject = subject
            };

            await this.appContext.CreateAsync(questionToAdd);
        }

        public async Task UpdateQuestionAsync(QuestionModel questionModel)
        {
            var question = await this.appContext.Questions.FirstOrDefaultAsync(x => x.Id == questionModel.Id);
            if(question != null)
            {
                question.PromptQuestion = questionModel.PromptQuestion;
                question.Subject = questionModel.Subject;

                await this.appContext.UpdateAsync(question);
            };
        }

        public async Task<Question> GetQuestionByIdAsync(int questionId)
        {
            var result = await this.appContext.Questions.FirstOrDefaultAsync(x => x.Id == questionId);
            return result;
        }

        public async Task<List<Question>> GetListAsync()
        {
            var results = await this.appContext.Questions.ToListAsync();
            return results;
        }

        public async Task DeleteQuestionAsync(Question question)
        {
            await this.appContext.DeleteAsync(question);
        }
    }
}
