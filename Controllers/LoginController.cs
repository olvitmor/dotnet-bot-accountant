using dotnet_bot_accountant.Interfaces;
using dotnet_bot_accountant.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_bot_accountant.Controllers;

[Route("login")]
public class LoginController : Controller
{
    #region Fields

    private readonly ILogger<LoginController> _logger;

    private readonly IUserManager _userManager;
    
    #endregion

    #region Constructor
    public LoginController(ILogger<LoginController> logger,
        IUserManager userManager)
    {
        _logger = logger;
        _userManager = userManager;
    }
    #endregion

    #region Actions

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Login(LoginModel authData)
    {
        if (!_userManager.TryLoginUser(HttpContext, authData, out string message))
        {
            ViewBag.ErrorMessage = message;

            return View(ViewBag);
        }

        return RedirectToAction(actionName:"Index", controllerName:"Home");
    }

    #endregion
}
