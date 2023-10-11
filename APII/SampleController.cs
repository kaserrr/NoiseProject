using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("apii/[controller]")]
[ApiController]
public class SampleController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello, World!");
    }

}
