namespace SenderApiInterface.RemoteAPI.Services;

public interface IVmExecutor
{
	Task<string> StartAsync(string vmName);
	Task<string> StopAsync(string vmName);
	Task<string> CreateAsync(string templateName);
	Task<string> DeleteAsync(string vmName);
}

public class MockVmExecutor : IVmExecutor
{
	public Task<string> StartAsync(string vmName)
		=> Task.FromResult($"[MOCK] would start VM '{vmName}'");
	public Task<string> StopAsync(string vmName)
		=> Task.FromResult($"[MOCK] would stop VM '{vmName}'");
	public Task<string> CreateAsync(string templateName)
		=> Task.FromResult($"[MOCK] would create VM from template '{templateName}'");
	public Task<string> DeleteAsync(string vmName)
		=> Task.FromResult($"[MOCK] would delete VM '{vmName}'");
}
