using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SenderApiInterface.Models;
using SenderApiInterface.RemoteAPI.Services;
using SenderApiInterface.Services;

namespace SenderApiInterface.Controllers;

[ApiController]
[Route("vm")]
public class VmController : ControllerBase
{
	private readonly IVmExecutor _exec;
	private readonly IOptions<SharedSecretOptions> _secret;

	public VmController(IVmExecutor exec, IOptions<SharedSecretOptions> secret)
	{
		_exec = exec;
		_secret = secret;
	}

	[HttpPost("start")]
	public async Task<IActionResult> Start([FromBody] StartVmRequest req)
	{
		if (string.IsNullOrWhiteSpace(req.VmName))
			return BadRequest(new { error = "VM name is required." });

		if (!PasswordValidator.IsValid(req.Password, _secret))
			return Unauthorized(new { error = "Неверный пароль." });

		var result = await _exec.StartAsync(req.VmName);
		return Ok(new { message = result });
	}

	[HttpPost("stop")]
	public async Task<IActionResult> Stop([FromBody] StopVmRequest req)
	{
		if (string.IsNullOrWhiteSpace(req.VmName))
			return BadRequest(new { error = "VM name is required." });

		if (!PasswordValidator.IsValid(req.Password, _secret))
			return Unauthorized(new { error = "Неверный пароль." });

		var result = await _exec.StopAsync(req.VmName);
		return Ok(new { message = result });
	}

	[HttpPost("create")]
	public async Task<IActionResult> Create([FromBody] CreateVmRequest req)
	{
		if (string.IsNullOrWhiteSpace(req.TemplateName))
			return BadRequest(new { error = "Template name is required." });

		if (!PasswordValidator.IsValid(req.Password, _secret))
			return Unauthorized(new { error = "Неверный пароль." });

		var result = await _exec.CreateAsync(req.TemplateName);
		return Ok(new { message = result });
	}

	[HttpDelete("delete")]
	public async Task<IActionResult> Delete([FromBody] DeleteVmRequest req)
	{
		if (string.IsNullOrWhiteSpace(req.VmName))
			return BadRequest(new { error = "VM name is required." });

		if (!PasswordValidator.IsValid(req.Password, _secret))
			return Unauthorized(new { error = "Неверный пароль." });

		var result = await _exec.DeleteAsync(req.VmName);
		return Ok(new { message = result });
	}
}
