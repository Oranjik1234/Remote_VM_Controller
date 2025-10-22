using System.CommandLine;
using VM_Hub.Security;

partial class Program
{
	static async Task<int> Main(string[] args)
	{
		var oldOption = new Option<string>("--old", "Введите текущий пароль") { IsRequired = true };
		var newOption = new Option<string>("--new", "Введите новый пароль") { IsRequired = true };

		var changePwdCmd = new Command("change-password", "Изменить пароль");
		changePwdCmd.AddOption(oldOption);
		changePwdCmd.AddOption(newOption);

		changePwdCmd.SetHandler((string oldPwd, string newPwd) =>
		{
			if (PasswordManager.TryChangePassword(oldPwd, newPwd))
				Console.WriteLine("Пароль изменён");
			else
				Console.WriteLine("Ошибка: неверный пароль");
		}, oldOption, newOption);

		var root = new RootCommand("VMHub CLI Tool");
		root.Add(changePwdCmd);

		return await root.InvokeAsync(args);
	}
}


