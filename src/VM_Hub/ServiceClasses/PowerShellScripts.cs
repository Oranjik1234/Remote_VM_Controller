using System.Diagnostics;
using VM_Hub.SystemMetrics;

namespace VM_Hub.Utilities
{
	public static class PowerShellScripts
	{
		private static readonly string SharedRoot = @"C:\VM_Shared"; //TODO : можно вынести в конфиг
		public static bool SetupSharedFolder(string vmId, string hostPath, string guestPath, bool readOnly, bool recheck = false)
		{
			if (!Directory.Exists(SharedRoot))
			{
				try
				{
					Directory.CreateDirectory(SharedRoot);
					if (!recheck)
						return SetupSharedFolder(vmId, hostPath, guestPath, readOnly, recheck: true);
					else
						return false;
				}
				catch
				{
					return false;
				}
			}
			if (string.IsNullOrWhiteSpace(hostPath))
				hostPath = Path.Combine(SharedRoot, vmId);

			if (!Directory.Exists(hostPath))
			{
				try
				{
					Directory.CreateDirectory(hostPath);
				}
				catch
				{
					return false;
				}
			}

			string readOnlyFlag = readOnly ? "--readonly" : "";
			string command = $"sharedfolder add \"{vmId}\" --name \"{guestPath}\" --hostpath \"{hostPath}\" --automount {readOnlyFlag}";

			return ExecuteVBoxManageCommand(command);
		}
		public static bool ConfigureVM(string vmId, VMInfo config)
		{
			string args = $"modifyvm \"{vmId}\" --cpus {config.CPU} --memory {config.RAM} --vram {config.DiskSize}";
			return ExecuteVBoxManageCommand(args);
		}
		public static VMInfo? PowerShellGetVMInfo(string vmId)
		{
			var args = $"showvminfo \"{vmId}\" --machinereadable";

			var process = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = "VBoxManage.exe",
					Arguments = args,
					RedirectStandardOutput = true,
					RedirectStandardError = true,
					UseShellExecute = false,
					CreateNoWindow = true
				}
			};

			try
			{
				process.Start();
				string output = process.StandardOutput.ReadToEnd();
				process.WaitForExit();

				if (process.ExitCode != 0)
					return null;

				var info = new VMInfo();

				foreach (var line in output.Split('\n'))
				{
					if (line.StartsWith("name="))
						info.Name = line.Split('=')[1].Trim('"');

					if (line.StartsWith("VMState="))
						info.State = line.Split('=')[1].Trim('"');

					if (line.StartsWith("memory="))
						info.RAM = int.Parse(line.Split('=')[1]);

					if (line.StartsWith("cpus="))
						info.CPU = int.Parse(line.Split('=')[1]);
				}

				return info;
			}
			catch
			{
				return null;
			}
		}
		private static bool ExecuteVBoxManageCommand(string args)
		{
			try
			{
				var process = new Process
				{
					StartInfo = new ProcessStartInfo
					{
						FileName = "VBoxManage.exe", 
						Arguments = args,
						RedirectStandardOutput = true,
						RedirectStandardError = true,
						UseShellExecute = false,
						CreateNoWindow = true
					}
				};

				process.Start();
				string output = process.StandardOutput.ReadToEnd();
				string error = process.StandardError.ReadToEnd();
				process.WaitForExit();

				return process.ExitCode == 0;
			}
			catch
			{
				return false;
			}
		}
	}
}
