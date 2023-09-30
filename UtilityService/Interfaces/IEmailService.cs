using QuestionExplorer.Models;

namespace QuestionExplorer.UtilityService.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(EmailModel emailModel);
    }
}
