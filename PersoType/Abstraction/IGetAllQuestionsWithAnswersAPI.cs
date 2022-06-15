using PersoType.Models.ResponseModels;
using Refit;

namespace PersoType.Abstraction
{
    public interface IGetAllQuestionsWithAnswersAPI
    {
        [Get("/api/CRUD_")]
        Task<IEnumerable<Question>> GetAllQuestionsWithAnswers();
    }
}
