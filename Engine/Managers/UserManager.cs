using dotnet_bot_accountant.Engine.Interfaces;

namespace dotnet_bot_accountant.Engine.Managers;

public class UserManager : IUserManager
{
    #region Fields
    private readonly ILogger<UserManager> _logger;
    private readonly IAuthManager _authManager;
    #endregion

    #region Constructors
    public UserManager(ILogger<UserManager> logger, IAuthManager authManager)
    {
        _logger = logger;
        _authManager = authManager;
    }
    #endregion

    #region Methods
    public bool TryLoginUser(HttpContext context, string username, string password, out string message)
    {
        return _authManager.TryAuthenticateUser(username, password, context, out message);
    }

    public void LogoutUser(HttpContext context)
    {

    }

    #endregion

}
