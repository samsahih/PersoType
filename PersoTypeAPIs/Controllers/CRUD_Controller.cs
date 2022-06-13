using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersoTypeAPIs.Models;
using PersoTypeAPIs.Repositories;

namespace PersoTypeAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUD_Controller : ControllerBase
    {
        private IRepositoryWrapper _repository;

        public CRUD_Controller(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuestions()
        {
            try
            {
                var questions = await _repository.Question.GetAllQuestionWithAnswersAsync();

                return Ok(questions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
                //return StatusCode(500, "Internal server error");
            }
        }
    }
}
