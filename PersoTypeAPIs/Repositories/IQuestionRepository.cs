using PersoTypeAPIs.Models;

namespace PersoTypeAPIs.Repositories
{
    public interface IQuestionRepository
    {
        void CreateQuestion(Question Question);
        void DeleteQuestion(Question Question);
        Task<IEnumerable<Question>> GetAllQuestionsAsync();
        Task<IEnumerable<Question>> GetAllQuestionWithAnswersAsync();
        Task<Question> GetQuestionByIdAsync(Guid QuestionId);
        Task<Question> GetQuestionWithAnswersAsync(Guid QuestionId);
        void UpdateQuestion(Question Question);
    }
}