using dotnet_bot_accountant.Engine.Interfaces;

namespace dotnet_bot_accountant.Engine.Managers;

public class AuthManager : IAuthManager
{
    #region Fields
    private readonly ILogger<AuthManager> _logger;
    private readonly IBlocksManager _blocksManager;
    #endregion

    #region Constructors
    public AuthManager(ILogger<AuthManager> logger, IBlocksManager blocksManager)
    {
        _logger = logger;
        _blocksManager = blocksManager;
    }
    #endregion

    #region Methods
    public bool TryAuthenticateUser(string username, string password, HttpContext context, out string message)
    {
        message = "test from AuthManager";
        return true;
    }

    public bool IsUserAuthenticated(HttpContext context)
    {
        return true;
    }
    #endregion
}
