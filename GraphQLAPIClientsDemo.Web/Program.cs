using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddGitHubGraphQLAPIClient()
    .ConfigureHttpClient(c => 
    {
        c.BaseAddress = new Uri("https://api.github.com/graphql");
        c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
    });

builder.Services
    .AddSpaceXGraphQLAPIClient()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://spacex-production.up.railway.app"));

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
