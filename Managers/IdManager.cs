using shortid;
using shortid.Configuration;

namespace dotnet_bot_accountant.Managers;

public static class IdManager
{
    private static readonly GenerationOptions _options = new GenerationOptions(useNumbers: true, length: 8);

    public static string NewId
    {
        get
        {
            return ShortId.Generate(_options);
        }
    }
}
