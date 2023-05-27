using dotnet_bot_accountant.Engine.Managers;
using dotnet_bot_accountant.Engine.Settings;

namespace dotnet_bot_accountant;

public class Shared
{
    public static XmlSettings Settings
    {
        get
        {
            return SettingsManager.Root;
        }
    }
}
