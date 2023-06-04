namespace dotnet_bot_accountant.Engine.Interfaces;

public interface IAuthManager
{
    public bool TryAuthenticateUser(string username, string password, HttpContext context, out string message);

    public bool IsUserAuthenticated(HttpContext context);
}
