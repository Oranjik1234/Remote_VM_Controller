using System.Management;
using System.Diagnostics;
using System.Threading;

public class SystemResources
{
	private PerformanceCounter _cpuCounter;
	private PerformanceCounter _ramAvailableCounter;
	public float CpuUsagePercent { get; set; }
	public float AvailableRamMB { get; set; }
	public long TotalRamMB { get; set; }
	public long FreeDiskGB { get; set; }

	public SystemResources(bool autoFill)
	{
		_cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
		_cpuCounter.NextValue();

		_ramAvailableCounter = new PerformanceCounter("Memory", "Available MBytes");
		_ramAvailableCounter.NextValue();
		if (autoFill)
		{
			Thread.Sleep(1000);
			CpuUsagePercent = GetCpuUsage();
			AvailableRamMB = GetAvailableRamInMB();
			TotalRamMB = GetTotalPhysicalMemoryInMB();
			FreeDiskGB = GetFreeDiskSpaceInGB();
		}
	}

	public float GetCpuUsage()
	{
		Thread.Sleep(1000);
		return _cpuCounter.NextValue();
	}

	public float GetAvailableRamInMB()
	{
		return _ramAvailableCounter.NextValue();
	}

	public long GetTotalPhysicalMemoryInMB()
	{
		var searcher = new ManagementObjectSearcher("SELECT TotalPhysicalMemory FROM Win32_ComputerSystem");
		foreach (var obj in searcher.Get())
		{
			return Convert.ToInt64(obj["TotalPhysicalMemory"]) / 1024 / 1024;
		}
		return 0;
	}

	public long GetFreeDiskSpaceInGB(string driveLetter = "C")
	{
		var drive = new DriveInfo(driveLetter);
		return drive.AvailableFreeSpace / (1024 * 1024 * 1024);
	}
}