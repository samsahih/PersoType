
namespace PersoTypeAPIs.Services
{
    public interface IPersonalityCalculations
    {
        Task<string> calculatePersonality(int[] answers);
    }
}