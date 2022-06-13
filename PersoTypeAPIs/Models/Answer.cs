using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PersoTypeAPIs.Models
{
    public class Answer
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        [ForeignKey("Question")]
        public int QuestionID { get; set; }
        public string Title { get; set; }
        public int Score { get; set; }
    }
}
