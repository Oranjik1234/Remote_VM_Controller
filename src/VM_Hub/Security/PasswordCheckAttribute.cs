using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace VM_Hub.Security
{
	public class RequirePasswordAttribute : Attribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var request = context.HttpContext.Request;
			if (!request.Query.TryGetValue("password", out var passwordValue))
			{
				context.Result = new UnauthorizedObjectResult("Пароль обязателен.");
				return;
			}

			string password = passwordValue!;
			if (!PasswordManager.CheckPassword(password))
			{
				context.Result = new UnauthorizedObjectResult("Неверный пароль.");
			}
		}
	}
}
