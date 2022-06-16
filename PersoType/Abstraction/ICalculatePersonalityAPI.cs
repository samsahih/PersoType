using PersoType.Models.ResponseModels;
using Refit;

namespace PersoType.Abstraction
{
    public interface ICalculatePersonalityAPI
    {
        [Post("/api/PersonalityApplicationData")]
        Task<string> calculatePersonality([Body] int[] answers);
    }
}
