using Microsoft.AspNetCore.Mvc;

namespace SenderApiInterface.RemoteAPI.Controllers;

[ApiController]
[Route("status")]
public class StatusController : ControllerBase
{
	[HttpGet("resources")]
	public IActionResult GetHostResources()
	{
		var info = new
		{
			Machine = Environment.MachineName,
			OS = System.Runtime.InteropServices.RuntimeInformation.OSDescription,
			CpuCount = Environment.ProcessorCount,
			WorkingSetMb = Math.Round(Environment.WorkingSet / 1024.0 / 1024.0, 1),
			Uptime = (DateTime.UtcNow - System.Diagnostics.Process.GetCurrentProcess().StartTime.ToUniversalTime()).ToString()
		};
		return Ok(info);
	}
}
