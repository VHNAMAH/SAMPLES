using Grape.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Grape.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CatsController : ControllerBase
{
    private readonly ICatsService _catsService;
    public CatsController(ICatsService catsService)
    {
        this._catsService = catsService;
    }

    [HttpGet("Fact")]
    public async Task<IActionResult> Get() {
        var catFact = await _catsService.FetchFact();
        
        if (catFact != null) {
            return Ok(catFact);
        }

        return NotFound();
    }
}
