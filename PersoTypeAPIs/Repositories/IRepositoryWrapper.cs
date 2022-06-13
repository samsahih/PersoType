namespace PersoTypeAPIs.Repositories
{
    public interface IRepositoryWrapper
    {
        IAnswerRepository Answer { get; }
        IQuestionRepository Question { get; }

        void Save();
    }
}