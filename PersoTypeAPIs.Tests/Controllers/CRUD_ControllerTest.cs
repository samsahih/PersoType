using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PersoTypeAPIs.Controllers;
using PersoTypeAPIs.Models;
using PersoTypeAPIs.Repositories;
using PersoTypeAPIs.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PersoTypeAPIs.Tests
{
    public class CRUD_ControllerTest
    {
        private ICrudOperations _crudOps;
        private CRUD_Controller _controller;
        private DbContextOptions<PersoTypeDbContext> _options;


        private void Populate(PersoTypeDbContext context)
        {
            // Seed question 1 with its answers
            Question quest1 = new Question()
            {
                Id = 1,
                Title = "You're really busy at work and a colleague is telling you their life story and personal woes. You:"
            };
            Answer quest1Ans1 = new Answer()
            {
                Id = 1,
                QuestionID = 1,
                Title = "Don't dare to interrupt them",
                Score = 1
            };
            Answer quest1Ans2 = new Answer()
            {
                Id = 2,
                QuestionID = 1,
                Title = "Think it's more important to give them some of your time; work can wait",
                Score = 2
            };
            Answer quest1Ans3 = new Answer()
            {
                Id = 3,
                QuestionID = 1,
                Title = "Listen, but with only with half an ear",
                Score = 3
            };
            Answer quest1Ans4 = new Answer()
            {
                Id = 4,
                QuestionID = 1,
                Title = "Interrupt and explain that you are really busy at the moment",
                Score = 4
            };

            // Seed question 2 with its answers
            Question quest2 = new Question()
            {
                Id = 2,
                Title = "You've been sitting in the doctor's waiting room for more than 25 minutes. You:"
            };
            Answer quest2Ans1 = new Answer()
            {
                Id = 5,
                QuestionID = 2,
                Title = "Look at your watch every two minutes",
                Score = 1
            };
            Answer quest2Ans2 = new Answer()
            {
                Id = 6,
                QuestionID = 2,
                Title = "Bubble with inner anger, but keep quiet",
                Score = 2
            };
            Answer quest2Ans3 = new Answer()
            {
                Id = 7,
                QuestionID = 2,
                Title = "Explain to other equally impatient people in the room that the doctor is always running late",
                Score = 3
            };
            Answer quest2Ans4 = new Answer()
            {
                Id = 8,
                QuestionID = 2,
                Title = "Complain in a loud voice, while tapping your foot impatiently",
                Score = 4
            };

            // Seed question 3 with its answers
            Question quest3 = new Question()
            {
                Id = 3,
                Title = "You're having an animated discussion with a colleague regarding a project that you're in charge of. You:"
            };
            Answer quest3Ans1 = new Answer()
            {
                Id = 9,
                QuestionID = 3,
                Title = "Don't dare contradict them",
                Score = 1
            };
            Answer quest3Ans2 = new Answer()
            {
                Id = 10,
                QuestionID = 3,
                Title = "Think that they are obviously right",
                Score = 2
            };
            Answer quest3Ans3 = new Answer()
            {
                Id = 11,
                QuestionID = 3,
                Title = "Defend your own point of view, tooth and nail",
                Score = 3
            };
            Answer quest3Ans4 = new Answer()
            {
                Id = 12,
                QuestionID = 3,
                Title = "Continuously interrupt your colleague",
                Score = 4
            };

            // add and save data to db
            context.Questions.Add(quest1);
            context.Questions.Add(quest2);
            context.Questions.Add(quest3);

            context.Answers.Add(quest1Ans1);
            context.Answers.Add(quest1Ans2);
            context.Answers.Add(quest1Ans3);
            context.Answers.Add(quest1Ans4);

            context.Answers.Add(quest2Ans1);
            context.Answers.Add(quest2Ans2);
            context.Answers.Add(quest2Ans3);
            context.Answers.Add(quest2Ans4);

            context.Answers.Add(quest3Ans1);
            context.Answers.Add(quest3Ans2);
            context.Answers.Add(quest3Ans3);
            context.Answers.Add(quest3Ans4);

            context.SaveChanges();
        }

        private void seedData()
        {
            var initContext = emptyDbData();

            Populate(initContext);
        }

        private PersoTypeDbContext emptyDbData()
        {
            _options = new DbContextOptionsBuilder<PersoTypeDbContext>()
                             .UseInMemoryDatabase(databaseName: "MockDB")
                             .Options;

            var initContext = new PersoTypeDbContext(_options);

            initContext.Database.EnsureDeleted();

            return initContext;
        }

        private ICrudOperations GetInMemoryRepository()
        {
            var options = new DbContextOptionsBuilder<PersoTypeDbContext>()
                             .UseInMemoryDatabase(databaseName: "MockDB")
                             .Options;

            /*var initContext = new PersoTypeDbContext(options);

            initContext.Database.EnsureDeleted();

            Populate(initContext);*/

            var testContext = new PersoTypeDbContext(options);
            var repositoryWrapper = new RepositoryWrapper(testContext);
            var crudOp = new CrudOperations(repositoryWrapper);

            return crudOp;
        }


        [SetUp]
        public void Setup()
        {
            _crudOps = GetInMemoryRepository();
            _controller = new CRUD_Controller(_crudOps);
        }

        [Test]
        public async Task GetAllQuestionsWithAnswers_WithNoData()
        {
            emptyDbData();

            var result = await _controller.GetAllQuestionsWithAnswers() as OkObjectResult;
            var model = result.Value as IEnumerable<Question>;

            Assert.AreEqual(0, model.Count());
            Assert.AreEqual(((int)HttpStatusCode.OK), result.StatusCode);
        }

        [Test]
        public async Task GetAllQuestionsWithAnswers_WithNormalData()
        {
            seedData();

            // Test logic
            var result = await _controller.GetAllQuestionsWithAnswers() as OkObjectResult;
            var model = result.Value as IEnumerable<Question>;

            Assert.AreEqual(3, model.Count());
            Assert.AreEqual("You're really busy at work and a colleague is telling you their life story and personal woes. You:", model.First().Title);
        }

        [Test]
        public async Task GetQuestionWithAnswersById()
        {
            // Arrange
            var expectedAnswersValue = new List<Answer>();
            expectedAnswersValue.Add(new Answer()
            {
                Id = 1,
                QuestionID = 1,
                Title = "Don't dare to interrupt them",
                Score = 1
            });
            expectedAnswersValue.Add(new Answer()
            {
                Id = 2,
                QuestionID = 1,
                Title = "Think it's more important to give them some of your time; work can wait",
                Score = 2
            });
            expectedAnswersValue.Add(new Answer()
            {
                Id = 3,
                QuestionID = 1,
                Title = "Listen, but with only with half an ear",
                Score = 3
            });
            expectedAnswersValue.Add(new Answer()
            {
                Id = 4,
                QuestionID = 1,
                Title = "Interrupt and explain that you are really busy at the moment",
                Score = 4
            });
            var expectedReturnValue = new Question()
            {
                Id = 1,
                Title = "You're really busy at work and a colleague is telling you their life story and personal woes. You:",
                Answers = expectedAnswersValue
            };

            // Seed data first
            seedData();

            // Test logic
            var result = await _controller.GetQuestionWithAnswersById(1) as OkObjectResult;
            var model = result.Value as Question;

            //Assert.AreEqual(expectedReturnValue, model);
            model.Should().BeEquivalentTo(expectedReturnValue);
        }

        [Test]
        public void CreateQuestion_IncorrectlyWithWrongAnswerScore()
        {
            // Arrange
            var expectedAnswersValue = new List<Answer>();
            expectedAnswersValue.Add(new Answer()
            {
                Id = 13,
                QuestionID = 4,
                Title = "Answer 1",
                Score = 1
            });
            expectedAnswersValue.Add(new Answer()
            {
                Id = 14,
                QuestionID = 4,
                Title = "Answer 2",
                Score = 2
            });
            expectedAnswersValue.Add(new Answer()
            {
                Id = 15,
                QuestionID = 4,
                Title = "Answer 3",
                Score = 7
            });
            expectedAnswersValue.Add(new Answer()
            {
                Id = 16,
                QuestionID = 4,
                Title = "Answer 4",
                Score = 4
            });
            var expectedReturnValue = new Question()
            {
                Id = 4,
                Title = "New question test !",
                Answers = expectedAnswersValue
            };

            // Seed data first
            seedData();

            // Test logic
            var result = _controller.CreateQuestion(expectedReturnValue) as BadRequestObjectResult;
            var model = result.Value as Question;

            Assert.AreEqual(((int)HttpStatusCode.BadRequest), result.StatusCode);
        }

        [Test]
        public void CreateQuestion_correctly()
        {
            // Arrange
            var expectedAnswersValue = new List<Answer>();
            expectedAnswersValue.Add(new Answer()
            {
                Id = 13,
                QuestionID = 4,
                Title = "Answer 1",
                Score = 1
            });
            expectedAnswersValue.Add(new Answer()
            {
                Id = 14,
                QuestionID = 4,
                Title = "Answer 2",
                Score = 2
            });
            expectedAnswersValue.Add(new Answer()
            {
                Id = 15,
                QuestionID = 4,
                Title = "Answer 3",
                Score = 2
            });
            expectedAnswersValue.Add(new Answer()
            {
                Id = 16,
                QuestionID = 4,
                Title = "Answer 4",
                Score = 4
            });
            var expectedReturnValue = new Question()
            {
                Id = 4,
                Title = "New question test !",
                Answers = expectedAnswersValue
            };

            // Seed data first
            seedData();

            // Test logic
            var result = _controller.CreateQuestion(expectedReturnValue) as CreatedAtRouteResult;
            var model = result.Value as Question;

            model.Should().BeEquivalentTo(expectedReturnValue);
            Assert.AreEqual(((int)HttpStatusCode.Created), result.StatusCode);
        }
    }
}