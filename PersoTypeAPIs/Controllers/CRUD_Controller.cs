using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersoTypeAPIs.Models;
using PersoTypeAPIs.Repositories;
using PersoTypeAPIs.Services;

namespace PersoTypeAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CRUD_Controller : ControllerBase
    {
        private ICrudOperations _crudOperations;

        public CRUD_Controller(ICrudOperations crudOperations)
        {
            _crudOperations = crudOperations;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuestionsWithAnswers()
        {
            try
            {
                var questions = await _crudOperations.GetApplicationQuestions();

                return Ok(questions);
            }
            catch (Exception ex)
            {
                //return StatusCode(500, ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "QuestionById")]
        public async Task<IActionResult> GetQuestionWithAnswersById(int id)
        {
            try
            {
                var question = await _crudOperations.GetQuestionById(id);

                if (question == null)
                    return NotFound();
                else
                    return Ok(question);
            }
            catch (Exception ex)
            {
                //return StatusCode(500, ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateQuestion([FromBody] Question question)
        {
            try
            {
                if (question == null)
                    return BadRequest("Question object is null");

                if (!ModelState.IsValid)
                    return BadRequest("Invalid object");

                var questionAddedStatus = _crudOperations.AddQuestion(question);
                
                if(questionAddedStatus.StatusCode == StatusCodes.Status405MethodNotAllowed)
                    return BadRequest("Please make sure your questions have scores from 1 to 4");

                return CreatedAtRoute("QuestionById", new { id = question.Id }, question);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
                //return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(int id, [FromBody] Question question)
        {
            try
            {
                if (question == null)
                    return BadRequest("Owner object is null");

                if (!ModelState.IsValid)
                    return BadRequest("Invalid object");

                var questionUpdatedStatus = await _crudOperations.UpdateQuestion(id, question);

                if (questionUpdatedStatus.StatusCode == StatusCodes.Status404NotFound)
                    return NotFound();

                if (questionUpdatedStatus.StatusCode == StatusCodes.Status405MethodNotAllowed)
                    return BadRequest("Please make sure your questions have scores from 1 to 4");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
                //return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            try
            {
                var questionUpdatedStatus = await _crudOperations.DeleteQuestion(id);

                if (questionUpdatedStatus.StatusCode == StatusCodes.Status404NotFound)
                    return NotFound();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
                //return StatusCode(500, "Internal server error");
            }
        }
    }
}
