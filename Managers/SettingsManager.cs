using dotnet_bot_accountant.Extensions;
using dotnet_bot_accountant.Interfaces;
using dotnet_bot_accountant.Xml;
using Serilog;

namespace dotnet_bot_accountant.Managers;

public class SettingsManager
{
    #region Fields

    public static XmlSettings Root { get; private set; } = new XmlSettings();

    private static IXmlManager<XmlSettings> _xmlManager = new XmlManager<XmlSettings>();

    private static Serilog.ILogger _logger = Log.Logger;

    private static string _path;

    private static string _prefix = "[xml]";

    #endregion

    #region Methods

    public static void Init()
    {
        _path = Paths.GetSettingsFilePath();
        ApplySettings("on startup");
    }

    public static void ApplySettings(string reason)
    {
        ReadXml();

        _logger.LogInfo($"{_prefix} settings applied. [{reason}]");
    }

    public static void SaveSettings(string reason)
    {
        WriteXml();

        _logger.LogInfo($"{_prefix} settings saved. [{reason}]");
    }

    private static void ReadXml()
    {
        CreateDefault();

        if (_xmlManager.ReadXml(_path, out var _root))
        {
            Root = _root;
            AfterRead();
        }
    }

    private static void WriteXml()
    {
        BeforeWrite();

        if (_xmlManager.WriteXml(_path, Root))
        {
            RaiseSaved();
        }
    }

    private static void BeforeWrite()
    {
        ProtectData();
    }

    private static void CreateDefault()
    {
        if (!File.Exists(_path))
        {
            _xmlManager.WriteXml(_path, new XmlSettings());
        }
    }

    private static void RaiseSaved()
    {

    }

    private static void AfterRead()
    {
        UnprotectData();
    }

    private static void ProtectData()
    {
        Root.TgBot.TokenProtected = ProtectionManager.EncryptString(Root.TgBot.Token);
        foreach(var usr in Root.Service.Users)
            usr.PasswordProtected = ProtectionManager.EncryptString(usr.PasswordProtected);
    }

    private static void UnprotectData()
    {
        Root.TgBot.Token = ProtectionManager.DecryptString(Root.TgBot.TokenProtected);
        
        foreach(var usr in Root.Service.Users)
            usr.Password = ProtectionManager.DecryptString(usr.PasswordProtected);
    }

    public static void ChangeXmlManager(IXmlManager<XmlSettings> xmlManager)
    {
        _xmlManager = xmlManager;
    }

    #endregion
}
