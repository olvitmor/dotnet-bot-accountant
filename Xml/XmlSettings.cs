using dotnet_bot_accountant.Engine.Enums;
using dotnet_bot_accountant.Managers;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;

namespace dotnet_bot_accountant.Xml;

[XmlRoot("Root")]
public class XmlSettings
{
    public AppSettings App { get; set; } = new AppSettings();

    public ServiceSettings Service { get; set; } = new ServiceSettings();

    public DbSettings Database { get; set; } = new DbSettings();

    [XmlElement("TelegramBot")]
    public TgBotSettings TgBot { get; set; } = new TgBotSettings();

    #region SubClasses

    [XmlRoot("Telegram")]
    public class TgBotSettings
    {
        [XmlAttribute]
        public bool Enabled { get; set; } = true;

        [XmlAttribute("Token")]
        public string TokenProtected { get; set; }

        [XmlIgnore]
        public string Token { get; set; }
    }

    [XmlRoot("Database")]
    public class DbSettings
    {
        [XmlAttribute]
        public DbType Type { get; set; }

        [XmlAttribute]
        public string Host { get; set; }

        [XmlAttribute]
        public int Port { get; set; }

        [XmlAttribute]
        public string DbName { get; set; }

        [XmlAttribute]
        public string UserName { get; set; }

        [XmlAttribute("Password")]
        public string PasswordProtected { get; set; }

        [XmlIgnore]
        public string Password { get; set; }
    }

    [XmlRoot("Service")]
    public class ServiceSettings
    {
        [XmlAttribute]
        public string Host { get; set; } = "localhost";

        [XmlAttribute]
        public int Port { get; set; } = 5001;

        public List<ServiceUser> Users { get; set; } = new List<ServiceUser>();

        [XmlIgnore]
        public string Url
        {
            get
            {
                return $"http://{Host}:{Port}";
            }
        }

        public ServiceSecurity Security { get; set; }
    }

    public class ServiceUser
    {
        [XmlAttribute]
        public string Id { get; set; } = IdManager.NewId;

        [XmlAttribute]
        public string Username { get; set; }

        [XmlAttribute("Password"), JsonIgnore]
        public string PasswordProtected { get; set; }

        [XmlIgnore, JsonIgnore]
        public string Password { get; set; }
    }

    public class ServiceSecurity
    {
        [XmlAttribute]
        public bool UseBlocks { get; set; } = true;

        [XmlAttribute]
        public int BlockAfterAttempt { get; set; } = 3;

        [XmlAttribute]
        public int MaxAttemtps { get; set; } = 10;

        [XmlAttribute]
        public int BlockPerAttempt { get; set; } = 300_000;// ms

        [XmlIgnore, JsonIgnore]
        public string PassPhrase { get; } = "olvitmor:1997";
    }

    [XmlRoot("App")]
    public class AppSettings
    {
        [XmlAttribute]
        public bool UseDb { get; set; } = false;

        [XmlIgnore]
        public bool UseXml
        {
            get
            {
                return !UseDb;
            }
        }
    }

    #endregion
}
