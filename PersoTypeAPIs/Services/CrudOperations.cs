using Microsoft.AspNetCore.Mvc;
using PersoTypeAPIs.Models;
using PersoTypeAPIs.Repositories;

namespace PersoTypeAPIs.Services
{
    public class CrudOperations : ICrudOperations
    {
        private IRepositoryWrapper _repository;

        public CrudOperations(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Question>> GetApplicationQuestions()
        {
            return await _repository.Question.GetAllQuestionWithAnswersAsync();
        }

        public async Task<Question> GetQuestionById(int id)
        {
            return await _repository.Question.GetQuestionWithAnswersAsync(id);
        }

        public async Task<StatusCodeResult> DeleteQuestion(int id)
        {
            var questionToDelete = await _repository.Question.GetQuestionWithAnswersAsync(id);
            
            if (questionToDelete == null)
                return new StatusCodeResult(StatusCodes.Status404NotFound);

            _repository.Question.DeleteQuestion(questionToDelete);
            _repository.Save();

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }

        public StatusCodeResult AddQuestion(Question questionToAdd)
        {
            if(AreAnswersLegit(questionToAdd.Answers) == false)
                return new StatusCodeResult(StatusCodes.Status405MethodNotAllowed);
            else
            {
                _repository.Question.CreateQuestion(questionToAdd);
                _repository.Save();

                return new StatusCodeResult(StatusCodes.Status201Created);
            }
        }

        public async Task<StatusCodeResult> UpdateQuestion(int id, Question question)
        {
            var questionToUpdate = await _repository.Question.GetQuestionWithAnswersAsync(id);

            if (questionToUpdate == null)
                return new StatusCodeResult(StatusCodes.Status404NotFound);

            if(AreAnswersLegit(question.Answers) == false)
                return new StatusCodeResult(StatusCodes.Status405MethodNotAllowed);

            question.Id = questionToUpdate.Id;
            _repository.Question.UpdateQuestion(question);
            _repository.Save();

            return new StatusCodeResult(StatusCodes.Status204NoContent);
        }

        public bool AreAnswersLegit(IEnumerable<Answer> answers)
        {
            if (answers.Any(d => d.Score < 1 || d.Score > 4))
                return false;

            return true;
        }
    }
}
