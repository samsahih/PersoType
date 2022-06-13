using PersoTypeAPIs.Models;

namespace PersoTypeAPIs.Repositories
{
    public interface IAnswerRepository
    {
        void CreateAnswer(Answer Answer);
        void DeleteAnswer(Answer Answer);
        Task<IEnumerable<Answer>> GetAllAnswersAsync();
        Task<Answer> GetAnswerByIdAsync(Guid AnswerId);
        void UpdateAnswer(Answer Answer);
    }
}