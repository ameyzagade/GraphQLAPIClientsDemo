using System.Net.Http.Headers;
using System.Reflection;
using GraphQLAPIClientsDemo.Infrastructure.Models;

var builder = WebApplication.CreateBuilder(args);

var secretsId = 
builder.Configuration
    .AddUserSecrets(Assembly.GetExecutingAssembly(), false);

var githubPrivatePassKey = builder.Configuration.GetValue<string>("GithubPrivatePassKey");

// get configuration
var graphQLClientsOptions = new GraphQLClientsOptions();
builder.Configuration
    .GetSection(nameof(GraphQLClientsOptions))
    .Bind(graphQLClientsOptions);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddGitHubGraphQLAPIClient()
    .ConfigureHttpClient(c => 
    {

        c.BaseAddress = new Uri(graphQLClientsOptions.GitHubGraphQLAPIEndpoint);
        c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", githubPrivatePassKey);
    });

builder.Services
    .AddSpaceXGraphQLAPIClient()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(graphQLClientsOptions.SpaceXGraphQLAPIEndpoint));

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

app.Run();
