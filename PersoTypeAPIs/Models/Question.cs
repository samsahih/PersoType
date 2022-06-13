using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PersoTypeAPIs.Models
{
    public class Question
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
