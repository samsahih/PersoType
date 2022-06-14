using Microsoft.AspNetCore.Mvc;
using PersoTypeAPIs.Models;

namespace PersoTypeAPIs.Services
{
    public interface ICrudOperations
    {
        Task<IEnumerable<Question>> GetApplicationQuestions();
        Task<Question> GetQuestionById(int id);
        Task<StatusCodeResult> DeleteQuestion(int id);
        StatusCodeResult AddQuestion(Question questionToAdd);
        Task<StatusCodeResult> UpdateQuestion(int id, Question question);
    }
}