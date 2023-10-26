using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ASP.NET_API.Models; // Make sure you import the Models namespace
using static ASP.NET_API.Models.Models;

[ApiController]
[Route("api/uplink")]
public class UplinkController : ControllerBase
{
    [HttpPost("parse")]
    public IActionResult ParseUplinkData([FromBody] List<UplinkMetaData> data)
    {
        try
        {
            if (data != null && data.Count > 0)
            {
                // Access the "bytes" property from the first element in the array.
                var bytesValue = data[0].phyPayload.macPayload.frmPayload?[0]?.bytes;

                if (!string.IsNullOrEmpty(bytesValue))
                {
                    return Ok("Hier is de bytes-waarde: " + bytesValue);
                }
            }

            return BadRequest("Invalid JSON data or missing 'bytes' property.");
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}