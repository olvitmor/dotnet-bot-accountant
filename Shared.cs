using dotnet_bot_accountant.Managers;
using dotnet_bot_accountant.Xml;

namespace dotnet_bot_accountant;

public class Shared
{
    public static XmlSettings Settings => SettingsManager.Root;
}
