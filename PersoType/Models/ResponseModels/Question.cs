namespace PersoType.Models.ResponseModels
{
    public class Question
    {
        public string Title { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
