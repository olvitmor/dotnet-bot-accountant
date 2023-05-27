namespace dotnet_bot_accountant.Engine.Interfaces;

public interface IXmlManager<T> where T : class
{
    public bool WriteXml(string path, T obj);

    public bool ReadXml(string path, out T obj);

    public bool IsFileChanged(string path, DateTime before);
}
