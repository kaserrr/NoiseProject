using Microsoft.AspNetCore.Mvc;

[Route("apii/[controller]")]
[ApiController]
public class SampleController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Hello, World!");
    }

    [HttpPost("receive-data")]
    public IActionResult ReceiveData([FromBody] MessageModel data)
    {
        if (data == null)
        {
            return BadRequest("Invalid data");
        }

        string name = data.Message;

        // Process the received data as needed
        return Ok($"Received data: Name = {name}");
    }
}
