using System.Net;

namespace dotnet_bot_accountant.Engine.Interfaces
{
    public interface IBlocksManager
    {
        public bool IsUserAllowed(IPAddress address, string username);

        public bool LoginBadAttempt(IPAddress address, string username, out string message);

        public void LoginGoodAttempt(IPAddress address, string username);
    }
}
