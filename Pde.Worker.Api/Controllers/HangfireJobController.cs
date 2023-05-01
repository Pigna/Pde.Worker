using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Pde.Worker.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HangfireJobController : ControllerBase
{
    private IBackgroundJobClient _backgroundJobClient;

    public HangfireJobController(IBackgroundJobClient backgroundJobClient)
    {
        _backgroundJobClient = backgroundJobClient;
    }

    [HttpGet(Name = "Get")]
    public IActionResult Get()
    {
        Console.WriteLine(_backgroundJobClient.Enqueue(() => Console.WriteLine("Hangfire Job!")));
        return new AcceptedResult();
    }
}