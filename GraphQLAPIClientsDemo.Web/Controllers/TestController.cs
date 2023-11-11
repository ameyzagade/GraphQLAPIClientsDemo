using GitHubGraphQLAPI.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceXGraphQLAPI.Client;

namespace GraphQLAPIClientsDemo.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    private readonly IGitHubGraphQLAPIClient _gitHubGraphQLAPIClient;
    private readonly ISpaceXGraphQLAPIClient _spaceXGraphQLAPIClient;

    public TestController(IGitHubGraphQLAPIClient gitHubGraphQLAPIClient,
                            ISpaceXGraphQLAPIClient spaceXGraphQLAPIClient)
    {
        _gitHubGraphQLAPIClient = gitHubGraphQLAPIClient;
        _spaceXGraphQLAPIClient = spaceXGraphQLAPIClient;
    }

    [HttpGet("github")]
    [AllowAnonymous]
    public async Task<GitHubGraphQLAPI.Client.IExampleQueryResult> GetGithubTestQueryData()
    {
        var result = await _gitHubGraphQLAPIClient.ExampleQuery.ExecuteAsync();
        return result?.Data;
    }

    [HttpGet("spacex")]
    [AllowAnonymous]
    public async Task<SpaceXGraphQLAPI.Client.IExampleQueryResult> GetSpaceXTestQueryData()
    {
        var result = await _spaceXGraphQLAPIClient.ExampleQuery.ExecuteAsync();
        return result?.Data;
    }
}