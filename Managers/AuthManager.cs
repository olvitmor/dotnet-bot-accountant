using dotnet_bot_accountant.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace dotnet_bot_accountant.Managers;

public class AuthManager : IAuthManager
{
    #region Fields
    private readonly ILogger<AuthManager> _logger;
    private readonly IBlocksManager _blocksManager;
    private readonly JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();
    #endregion

    #region Constructors
    public AuthManager(ILogger<AuthManager> logger, IBlocksManager blocksManager)
    {
        _logger = logger;
        _blocksManager = blocksManager;
    }
    #endregion

    #region Methods
    public bool AuthenticateUser(string username, string password, HttpContext context, out string message)
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
