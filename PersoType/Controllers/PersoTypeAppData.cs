using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PersoType.Abstraction;
using PersoType.Models.ResponseModels;
using PersoType.Models.SettingsModels;
using Refit;

namespace PersoType.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersoTypeAppData : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private string dataBaseUrl = "";

        public PersoTypeAppData(IOptions<AppSettings> options)
        {
            this._appSettings = options.Value;
            dataBaseUrl = _appSettings.ConfigSettings.PersoTypeAPIsURL;
        }

        // This gets data for the Value Streams
        [HttpPost("GetAllQuestions")]
        public async Task<IEnumerable<Question>> GetAllQuestions()
        {
            IGetAllQuestionsWithAnswersAPI dataService = RestService.For<IGetAllQuestionsWithAnswersAPI>(dataBaseUrl);

            var dataResponse = await dataService.GetAllQuestionsWithAnswers();

            return dataResponse;
        }

        // This gets data for the Value Streams
        [HttpPost("GetPersonalityType")]
        public async Task<string> GetPersonalityType([FromForm] List<int> answers)
        {
            ICalculatePersonalityAPI dataService = RestService.For<ICalculatePersonalityAPI>(dataBaseUrl);

            var dataResponse = await dataService.calculatePersonality(answers.ToArray());

            return dataResponse;
        }
    }
}
