using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Pde.Worker.Core.Services;

namespace Pde.Worker.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HangfireJobController : ControllerBase
{
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly ITestService _testService;

    public HangfireJobController(IBackgroundJobClient backgroundJobClient, ITestService testService)
    {
        _backgroundJobClient = backgroundJobClient;
        _testService = testService;
    }

    [HttpGet(Name = "Get")]
    public IActionResult Get()
    {
        Console.WriteLine(_backgroundJobClient.Enqueue(() => _testService.Test("Hangfire!")));
        return new AcceptedResult();
    }
}