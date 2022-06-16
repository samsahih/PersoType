using PersoTypeAPIs.Models;
using PersoTypeAPIs.Repositories;

namespace PersoTypeAPIs.Services
{
    public class PersonalityCalculations : IPersonalityCalculations
    {
        private IRepositoryWrapper _repository;

        public PersonalityCalculations(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<string> calculatePersonality(int[] answers)
        {
            var allQuestions = await _repository.Question.GetAllQuestionWithAnswersAsync();

            var score = 0;
            var introvertScore = allQuestions.Count();
            var extrovertScore = allQuestions.Count() * 4;
            for (int i = 0; i < answers.Length; i++)
            {
                var currentAnswer = allQuestions.ElementAt(i).Answers.ElementAt(answers[i]);
                score += currentAnswer.Score;
            }

            return buildResultData(score, introvertScore, extrovertScore);
        }

        String buildResultData(int score, int introvert, int extrovert)
        {
            if (score == introvert)
            {
                return "INTROVERT";
            }
            else if (score > introvert &&
              score <= ((extrovert - introvert) / 2 + introvert))
            {
                return "Both, but more INTROVERT";
            }
            else if (score >= ((extrovert - introvert) / 2 + introvert) &&
              score < extrovert)
            {
                return "Both, but more EXTROVERT";
            }
            else
            {
                return "EXTROVERT";
            }
        }
    }
}
