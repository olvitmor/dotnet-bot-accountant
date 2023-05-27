using dotnet_bot_accountant.Engine.Interfaces;
using dotnet_bot_accountant.Engine.Settings;
using dotnet_bot_accountant.Extensions;
using Serilog;

namespace dotnet_bot_accountant.Engine.Managers;

public class SettingsManager
{
    #region Fields

    public static XmlSettings Root { get; private set; } = new XmlSettings();

    private static IXmlManager<XmlSettings> _xmlManager = new XmlManager<XmlSettings>();

    private static Serilog.ILogger _logger = Log.Logger;

    private static string _path;

    #endregion

    #region Methods
    
    public static void Init()
    {
        ApplySettings("on startup");
    }

    public static void ApplySettings(string reason)
    {
        _path = Paths.GetSettingsFilePath();
        ReadXml();

        _logger.LogInfo($"settings applied. [{reason}]");
    }

    private static void ReadXml()
    {
        CreateDefault();

        if (_xmlManager.ReadXml(_path, out var _root))
        {
            Root = _root;
            AfterRead();
        }

        WriteXml();
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
        Root.Service.PasswordProtected = ProtectionManager.EncryptString(Root.Service.Password);
        Root.Database.PasswordProtected = ProtectionManager.EncryptString(Root.Service.Password);
        Root.TgBot.TokenProtected = ProtectionManager.EncryptString(Root.TgBot.Token);
    }

    private static void UnprotectData()
    {
        Root.Service.Password = ProtectionManager.DecryptString(Root.Service.PasswordProtected);
        Root.Database.Password = ProtectionManager.DecryptString(Root.Service.PasswordProtected);
        Root.TgBot.Token = ProtectionManager.DecryptString(Root.TgBot.TokenProtected);
    }

    public static void ChangeXmlManager(IXmlManager<XmlSettings> xmlManager)
    {
        _xmlManager = xmlManager;
    }

    #endregion
}
