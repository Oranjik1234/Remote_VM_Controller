using Microsoft.Extensions.Options;

namespace SenderApiInterface.Services;

public class SharedSecretOptions
{
	public string? SharedSecret { get; set; }
}

public static class PasswordValidator
{
	public static bool IsValid(string? pwd, IOptions<SharedSecretOptions> opt)
		=> !string.IsNullOrWhiteSpace(pwd) && pwd == opt.Value.SharedSecret;
}
