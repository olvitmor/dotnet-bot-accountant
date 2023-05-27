using System.Text;
using System.Text.RegularExpressions;

namespace dotnet_bot_accountant.Engine.Managers;

public class ProtectionManager
{
    public static string EncryptString(string input)
    {
        var bytes = Encoding.UTF8.GetBytes(input);
        var res = Convert.ToBase64String(bytes);
        return res;
    }

    public static string DecryptString(string input)
    {
        if (string.IsNullOrEmpty(input))
            return "";

        if (!IsBase64String(input))
            return input;

        var bytes = Convert.FromBase64String(input);
        var res = Encoding.UTF8.GetString(bytes);

        return res;
    }

    private static bool IsBase64String(string base64)
    {
        base64 = base64.Trim();
        return (base64.Length % 4 == 0) && Regex.IsMatch(base64, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
    }
}
