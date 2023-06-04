namespace dotnet_bot_accountant.Engine.Interfaces;

public interface IUserManager
{
    public bool TryLoginUser(HttpContext context, string username, string password, out string message);

    public void LogoutUser(HttpContext context);
}
