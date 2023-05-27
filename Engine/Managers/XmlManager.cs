using dotnet_bot_accountant.Engine.Interfaces;
using dotnet_bot_accountant.Extensions;
using Serilog;
using System.Xml.Serialization;

namespace dotnet_bot_accountant.Engine.Managers;

public class XmlManager<T>: IXmlManager<T> where T : class
{
    private readonly Serilog.ILogger _logger = Log.Logger;
    private readonly XmlSerializer _xmlSerializer = new XmlSerializer(typeof(T));
    private readonly string _type = "xml";

    public bool WriteXml(string path, T obj)
    {
        try
        {
            using (TextWriter stream = new StreamWriter(path))
            {
                _xmlSerializer.Serialize(stream, obj);
                stream.Close();
            }
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError($"[{_type}] error saving object", ex);
            return false;
        }
    }

    public bool ReadXml(string path, out T obj)
    {
        obj = default(T);
        try
        {
            using (TextReader stream = new StreamReader(path))
            {
                obj = (T)_xmlSerializer.Deserialize(stream);
                stream.Close();
            }
            return true;
        }
        catch (Exception ex)
        {
            var fileName = Path.GetFileName(path);
            _logger.LogError($"[{_type}] error reading file {fileName}", ex);
            return false;
        }
    }

    public bool IsFileChanged(string path, DateTime before)
    {
        return false;
    }
}
