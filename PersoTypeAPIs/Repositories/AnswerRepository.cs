using Microsoft.EntityFrameworkCore;
using PersoTypeAPIs.GenericRepositories;
using PersoTypeAPIs.Models;

namespace PersoTypeAPIs.Repositories
{
    public class AnswerRepository : RepositoryBase<Answer>, IAnswerRepository
    {
        public AnswerRepository(PersoTypeDbContext PersoTypeDbContext)
        : base(PersoTypeDbContext)
        {

        }

        public async Task<IEnumerable<Answer>> GetAllAnswersAsync()
        {
            return await FindAll()
               //.OrderBy(ow => ow.Title)
               .ToListAsync();
        }
        public async Task<Answer> GetAnswerByIdAsync(Guid AnswerId)
        {
            return await FindByCondition(Answer => Answer.Id.Equals(AnswerId))
                .FirstOrDefaultAsync();
        }
        public void CreateAnswer(Answer Answer)
        {
            Create(Answer);
        }
        public void UpdateAnswer(Answer Answer)
        {
            Update(Answer);
        }
        public void DeleteAnswer(Answer Answer)
        {
            Delete(Answer);
        }
    }
}
