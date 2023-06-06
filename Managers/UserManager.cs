using dotnet_bot_accountant.Interfaces;
using dotnet_bot_accountant.Models;
using dotnet_bot_accountant.Xml;

namespace dotnet_bot_accountant.Managers;

public class UserManager : IUserManager
{
    #region Fields
    private readonly ILogger<UserManager> _logger;
    private readonly IAuthManager _authManager;
    private List<XmlSettings.ServiceUser> _serviceUsers => Shared.Settings.Service.Users;
    #endregion

    #region Constructors
    public UserManager(ILogger<UserManager> logger, IAuthManager authManager)
    {
        _logger = logger;
        _authManager = authManager;
    }
    #endregion

    #region Methods
    public bool TryLoginUser(HttpContext context, LoginModel data, out string message)
    {
        return _authManager.AuthenticateUser(data.Username, data.Password, context, out message);
    }

    public void LogoutUser(HttpContext context)
    {

    }

    //public bool CreateFirstUser(HttpContext context, CreateFirstUserModel data, out string message)
    //{
    //    if (IsAnyUserExists())
    //    {
    //        message = "at least 1 user already exists";
    //        return false;
    //    }

    //    if (data.PassPhrase != Shared.Settings.Service.Security.PassPhrase)
    //    {
    //        message = "wrong pass phrase";
    //        return false;
    //    }

    //    var user = new XmlSettings.ServiceUser()
    //    {
    //        Username = data.Username,
    //        Password = data.Password,
    //    };

    //    _serviceUsers.Add(user);

    //    SettingsManager.SaveSettings("first user created");
    //}

    public bool IsAnyUserExists()
    {
        return _serviceUsers.Count > 0;
    }

    #endregion

}
