using PersoTypeAPIs.Models;

namespace PersoTypeAPIs.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private PersoTypeDbContext _repoContext;
        private IQuestionRepository _Question;
        private IAnswerRepository _Answer;
        public IQuestionRepository Question
        {
            get
            {
                if (_Question == null)
                {
                    _Question = new QuestionRepository(_repoContext);
                }
                return _Question;
            }
        }

        public IAnswerRepository Answer
        {
            get
            {
                if (_Answer == null)
                {
                    _Answer = new AnswerRepository(_repoContext);
                }
                return _Answer;
            }
        }

        public RepositoryWrapper(PersoTypeDbContext PersoTypeDbContext)
        {
            _repoContext = PersoTypeDbContext;
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
