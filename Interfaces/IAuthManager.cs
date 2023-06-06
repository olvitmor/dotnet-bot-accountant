namespace dotnet_bot_accountant.Interfaces;

public interface IAuthManager
{
    public bool AuthenticateUser(string username, string password, HttpContext context, out string message);

    public bool IsUserAuthenticated(HttpContext context);
}
