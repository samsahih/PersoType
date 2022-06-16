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
    public class PersonalityApplicationDataControllerTest
    {
        private IPersonalityCalculations _calculator;
        private IRepositoryWrapper _repoWrapper;
        private DbContextOptions<PersoTypeDbContext> _options;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<PersoTypeDbContext>()
                             .UseInMemoryDatabase(databaseName: "MockDB")
                             .Options;

            var initContext = new PersoTypeDbContext(_options);
            _repoWrapper = new RepositoryWrapper(initContext);
            _calculator = new PersonalityCalculations(_repoWrapper);
        }

        [Test]
        public async Task CalculatePersonality_With3QuestionsAndScoreOf3()
        {
            // Arange
            string expectedResult = "INTROVERT";


            // Test logic
            var result = await _calculator.calculatePersonality(new int[3] { 0, 0, 0 }) as string;
            var model = result as string;

            Assert.AreEqual(expectedResult, model.ToString());
        }

        [Test]
        public async Task CalculatePersonality_With3QuestionsAndScoreBetween3And7()
        {
            // Arange
            string expectedResult = "Both, but more INTROVERT";


            // Test logic
            var result = await _calculator.calculatePersonality(new int[3] { 1, 2, 1 }) as string;
            var model = result as string;

            Assert.AreEqual(expectedResult, model.ToString());
        }

        [Test]
        public async Task CalculatePersonality_With3QuestionsAndScoreBetween8And12()
        {
            // Arange
            string expectedResult = "Both, but more EXTROVERT";


            // Test logic
            var result = await _calculator.calculatePersonality(new int[3] { 3, 2, 3 }) as string;
            var model = result as string;

            Assert.AreEqual(expectedResult, model.ToString());
        }

        [Test]
        public async Task CalculatePersonality_With3QuestionsAndScore12()
        {
            // Arange
            string expectedResult = "EXTROVERT";


            // Test logic
            var result = await _calculator.calculatePersonality(new int[3] { 3, 3, 3 }) as string;
            var model = result as string;

            Assert.AreEqual(expectedResult, model.ToString());
        }
    }
}