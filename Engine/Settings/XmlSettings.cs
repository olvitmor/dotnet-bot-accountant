﻿using dotnet_bot_accountant.Engine.Enums;
using System.Xml.Serialization;

namespace dotnet_bot_accountant.Engine.Settings;

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

        [XmlAttribute]
        public string UserName { get; set; } = "olvitmor";

        [XmlAttribute("Password")]
        public string PasswordProtected { get; set; }

        [XmlIgnore]
        public string Password { get; set; }

        [XmlIgnore]
        public string Url
        {
            get
            {
                return $"http://{Host}:{Port}";
            }
        }
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