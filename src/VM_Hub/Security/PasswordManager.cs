using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace VM_Hub.Security
{
	public class PasswordRecord
	{
		public string Salt { get; set; }
		public string Hash { get; set; }
	}

	public static class PasswordManager
	{
		private static readonly string FilePath = Path.Combine(AppContext.BaseDirectory, "password.json");
		public static bool TryChangePassword(string currentPassword, string newPassword)
		{
			if (!File.Exists(FilePath)) return false; // TODO: Нормально логировать ошибку 

			var json = File.ReadAllText(FilePath);
			var record = JsonSerializer.Deserialize<PasswordRecord>(json);

			var inputHash = ComputeHash(currentPassword, record.Salt);
			if (inputHash != record.Hash)
			{
				return false; // TODO: логировать ошибку замены из-за неправильного пароля
			}

			SetPassword(newPassword);
			return true;
		}
		public static void SetPassword(string newPassword)
		{
			var salt = GenerateSalt();
			var hash = ComputeHash(newPassword, salt);

			var record = new PasswordRecord { Salt = salt, Hash = hash };
			var json = JsonSerializer.Serialize(record);

			File.WriteAllText(FilePath, json);
		}

		public static bool CheckPassword(string input)
		{
			if (!File.Exists(FilePath))
				return false; //TODO: Сделать нормальный отчет об ошибке

			var json = File.ReadAllText(FilePath);
			var record = JsonSerializer.Deserialize<PasswordRecord>(json);

			if (record == null)//хз этого быть не может из за базового пароля если файл не чистили
				return false;//TODO: Сделать нормальный отчет об ошибке

			var inputHash = ComputeHash(input, record.Salt);
			return inputHash == record.Hash;
		}

		private static string GenerateSalt()
		{
			var bytes = new byte[16];
			using var rng = RandomNumberGenerator.Create();
			rng.GetBytes(bytes);
			return Convert.ToBase64String(bytes);
		}
		private static string ComputeHash(string password, string salt)
		{
			using var sha = SHA256.Create();
			var combined = Encoding.UTF8.GetBytes(password + salt);
			var hash = sha.ComputeHash(combined);
			return Convert.ToBase64String(hash);
		}
	}
}