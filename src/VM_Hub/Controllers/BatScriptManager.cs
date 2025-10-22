using System.Diagnostics;

namespace VM_Hub.Controllers
{
	public  class BatScriptManager
	{
		private readonly string path = Path.Combine(AppContext.BaseDirectory, "Scripts");
		public string CreateVM()
		{
			return RunScript(Path.Combine(path, "CreateVM.bat"));

		}
		public string DeleteVM()
		{
			return RunScript(Path.Combine(path, "DeleteVM.bat"));
		}
		public string StartVM()
		{
			return RunScript(Path.Combine(path, "StartVM.bat"));
		}
		public string StopVM()
		{
			return RunScript(Path.Combine(path, "StopVM.bat"));
		}
		private  string RunScript(string path)
		{
			try
			{
				var process = new Process
				{
					StartInfo = new ProcessStartInfo
					{
						FileName = path,
						UseShellExecute = false,
						RedirectStandardOutput = true,
						RedirectStandardError = true,
						CreateNoWindow = true
					}
				};

				process.Start();

				string output = process.StandardOutput.ReadToEnd();
				string error = process.StandardError.ReadToEnd();

				process.WaitForExit();

				return string.IsNullOrEmpty(error) ? output : $"ERROR: {error}";
			}
			catch (Exception ex)
			{
				return $"EXCEPTION: {ex.Message}";
			}
		}
	}
}
