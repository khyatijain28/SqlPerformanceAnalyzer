using Microsoft.AspNetCore.Mvc;
using SqlPerformanceAnalyzer.Models;
using SqlPerformanceAnalyzer.Services;

namespace SqlPerformanceAnalyzer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyzerController : ControllerBase
{
    private readonly SqlAnalyzerService _service;

    public AnalyzerController(SqlAnalyzerService service)
    {
        _service = service;
    }

    [HttpPost]
    public IActionResult Analyze([FromBody] QueryRequest request)
    {
        var result = _service.Analyze(request.Query);

        return Ok(result);
    }
}