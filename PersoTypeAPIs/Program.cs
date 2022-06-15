using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersoTypeAPIs.Models;
using PersoTypeAPIs.Repositories;
using PersoTypeAPIs.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var _configuration = builder.Services.BuildServiceProvider().GetService<IConfiguration>();
string connectionString = _configuration.GetConnectionString("PersoTypeDB");
builder.Services.AddDbContext<PersoTypeDbContext>(option => option.UseInMemoryDatabase(connectionString));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
        builder.SetIsOriginAllowed(_ => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

// DIs
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
builder.Services.AddScoped<ICrudOperations, CrudOperations>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


// Seed data on start of app to have some 3 default questions to test with
//var context = app.Services.GetService<PersoTypeDbContext>();
var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetService<PersoTypeDbContext>();
SeedData(context);

app.Run();


// Seed data
static void SeedData(PersoTypeDbContext context)
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
