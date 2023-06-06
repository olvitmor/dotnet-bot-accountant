using dotnet_bot_accountant.Models;

namespace dotnet_bot_accountant.Interfaces;

public interface IUserManager
{
    public bool TryLoginUser(HttpContext context, LoginModel data, out string message);

    public void LogoutUser(HttpContext context);

    public bool IsAnyUserExists();
}
