using dotnet_bot_accountant.Engine.Interfaces;
using dotnet_bot_accountant.Engine.Models;
using System.Net;

namespace dotnet_bot_accountant.Engine.Managers;

public class BlocksManager : IBlocksManager
{
    #region Fields
    private readonly ILogger<BlocksManager> _logger;
    private List<BlockModel> _blocks = new List<BlockModel>();
    #endregion

    #region Constructors
    public BlocksManager(ILogger<BlocksManager> logger)
    {
        _logger = logger;
    }
    #endregion

    #region Methods
    public bool IsUserAllowed(IPAddress address, string username)
    {
        return true;
    }

    public bool LoginBadAttempt(IPAddress address, string username, out string message)
    {
        message = "test from BlocksManager";
        return false;
    }

    public void LoginGoodAttempt(IPAddress address, string username)
    {

    }
    #endregion
}