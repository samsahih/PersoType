using Microsoft.AspNetCore.Mvc;
using PersoTypeAPIs.Services;

namespace PersoTypeAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalityApplicationDataController : ControllerBase
    {
        private readonly IPersonalityCalculations _calculator;

        public PersonalityApplicationDataController(IPersonalityCalculations calculator)
        {
            _calculator = calculator;
        }

        [HttpPost]
        public async Task<IActionResult> CalculatePersonality([FromBody] int[] answers)
        {
            try
            {
                var personalityResult = await _calculator.calculatePersonality(answers);

                return Ok(personalityResult);
            }
            catch (Exception ex)
            {
                //return StatusCode(500, ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
