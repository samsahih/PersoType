using Microsoft.EntityFrameworkCore;
using PersoTypeAPIs.GenericRepositories;
using PersoTypeAPIs.Models;

namespace PersoTypeAPIs.Repositories
{
    public class QuestionRepository : RepositoryBase<Question>, IQuestionRepository
    {
        public QuestionRepository(PersoTypeDbContext PersoTypeDbContext)
        : base(PersoTypeDbContext)
        {

        }

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            return await FindAll()
               //.OrderBy(q => q.Title)
               .ToListAsync();
        }
        public async Task<IEnumerable<Question>> GetAllQuestionWithAnswersAsync()
        {
            return await FindAll()
                .Include(an => an.Answers)
                .ToListAsync();
        }
        public async Task<Question> GetQuestionByIdAsync(Guid QuestionId)
        {
            return await FindByCondition(Question => Question.Id.Equals(QuestionId))
                .FirstOrDefaultAsync();
        }
        public async Task<Question> GetQuestionWithAnswersAsync(Guid QuestionId)
        {
            return await FindByCondition(Question => Question.Id.Equals(QuestionId))
                .Include(an => an.Answers)
                .FirstOrDefaultAsync();
        }
        public void CreateQuestion(Question Question)
        {
            Create(Question);
        }
        public void UpdateQuestion(Question Question)
        {
            Update(Question);
        }
        public void DeleteQuestion(Question Question)
        {
            Delete(Question);
        }
    }
}
