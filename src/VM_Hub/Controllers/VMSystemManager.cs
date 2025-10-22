using VM_Hub.SystemMetrics;
using VM_Hub.Utilities;
namespace VM_Hub.Services
{
	public class VMSystemManager
	{
		public SystemResources GetHostResources()
		{
			return new SystemResources(true);
		}

		public bool ConfigureSharedFolder(string vmId, string hostPath, string guestPath, bool readOnly)
		{
			return PowerShellScripts.SetupSharedFolder(vmId, hostPath, guestPath, readOnly, true);
		}

		public VMInfo GetVMInfo(string vmId)
		{
			return PowerShellScripts.PowerShellGetVMInfo(vmId);
		}

		public bool UpdateVMConfig(string vmId, VMInfo config)
		{
			return PowerShellScripts.ConfigureVM(vmId, config);
		}
	}
}
