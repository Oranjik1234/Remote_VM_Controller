using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VM_Hub.ConfigClasses;
using VM_Hub.Security;
using VM_Hub.Services;
using VM_Hub.SystemMetrics;

namespace VM_Hub.Controllers
{

	[ApiController]
	[Route("vm")]
	public class VMController : ControllerBase
	{
		private readonly HubSettings _settings;
		public VMController(IOptions<HubSettings> options)
		{
			_settings = options.Value;
		}
		[RequirePassword]
		[HttpPost("start")]
		public IActionResult StartVM([FromBody] string vmName)
		{
			if (string.IsNullOrWhiteSpace(vmName))
				return BadRequest("VM name is required.");

			string result = new BatScriptManager().StartVM();

			return Ok($"Start request for VM '{vmName}' received.");
		}
		[RequirePassword]
		[HttpPost("stop")]
		public IActionResult StopVM([FromBody] string vmName)
		{
			if (string.IsNullOrWhiteSpace(vmName))
				return BadRequest("VM name is required.");

			string result = new BatScriptManager().StopVM();

			return Ok($"Stop request for VM '{vmName}' received.");
		}
		[RequirePassword]
		[HttpPost("create")]
		public IActionResult CreateVM([FromBody] string templateName)
		{
			if (string.IsNullOrWhiteSpace(templateName))
				return BadRequest("Template name is required.");

			string result = new BatScriptManager().CreateVM();
			return Ok($"Create request with template '{templateName}' received.");
		}
		[RequirePassword]
		[HttpDelete("delete")]
		public IActionResult DeleteVM([FromBody] string vmName)
		{
			if (string.IsNullOrWhiteSpace(vmName))
				return BadRequest("VM name is required.");

			string result = new BatScriptManager().DeleteVM();
			return Ok($"Delete request for VM '{vmName}' received.");
		}
		// TODO: �������� ConfigureVmRequest �������� - ��������� � �������� ���� ���
		//public class ConfigureVmRequest
		//{
		//	public string VMName { get; set; }
		//	public int CPU { get; set; }
		//	public int RAM { get; set; } // In MB
		//	public List<string> SharedFolders { get; set; }
		//}

		[HttpGet("info-VM")]
		public IActionResult GetVMInfo([FromQuery] string vmName)
		{
			if (string.IsNullOrWhiteSpace(vmName))
				return BadRequest("��� �� �����������.");

			var manager = new VMSystemManager();
			var info = manager.GetVMInfo(vmName);

			if (info == null)
				return NotFound("����������� ������ �� ������� ��� ��������� ������.");

			return Ok(info);
		}
		[HttpPost("configure-shared-folder")]
		public IActionResult ConfigureSharedFolder(
			[FromQuery] string vmId,
			[FromQuery] string hostPath,
			[FromQuery] string guestPath,
			[FromQuery] bool readOnly = false)
		{
			if (string.IsNullOrWhiteSpace(vmId))
				return BadRequest("VM ID �� ������.");

			if (string.IsNullOrWhiteSpace(hostPath) || !Directory.Exists(hostPath))
				return BadRequest("���� � ����� ����� �� ����� �� ������ ��� �� ����������.");

			if (string.IsNullOrWhiteSpace(guestPath))
				return BadRequest("���� � ����� ����� � �������� ������� �� ������.");

			var vMSystemManager = new VMSystemManager();

			bool result = vMSystemManager.ConfigureSharedFolder(vmId, hostPath, guestPath, readOnly);

			if (result)
				return Ok("����� ����� ������� ���������.");
			else
				return StatusCode(500, "�� ������� ��������� ����� �����.");
		}

		[RequirePassword]
		[HttpPost("update-VM-config")]
		public IActionResult UpdateVMConfig([FromQuery] string vmName, [FromBody] VMInfo config)
		{
			if (string.IsNullOrWhiteSpace(vmName))
				return BadRequest("VM Name ����������.");

			var manager = new VMSystemManager();
			bool success = manager.UpdateVMConfig(vmName, config);

			return success ? Ok("������������ ���������.") : StatusCode(500, "������ ����������.");
		}
	}

	[ApiController]
	[Route("status")]
	public class StatusController : ControllerBase
	{
		[HttpGet("resources")]
		public IActionResult GetHostResources()
		{
			return Ok(new SystemResources(true));
		}
	}
}