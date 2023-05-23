using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Pde.Worker.Core.Contracts;
using Pde.Worker.Core.Services;

namespace Pde.Worker.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HangfireJobController : ControllerBase
{
    private readonly IBackgroundJobClient _backgroundJobClient;
    private readonly IExportService _exportService;

    public HangfireJobController(IBackgroundJobClient backgroundJobClient, IExportService exportService)
    {
        _backgroundJobClient = backgroundJobClient;
        _exportService = exportService;
    }

    [HttpPost]
    public async Task<OkObjectResult> DatabaseExportJob([FromBody] SubmitExportDataRequest request)
    {
        return Ok(_backgroundJobClient.Enqueue(() => _exportService.SubmitExportData(request)));
    }
}