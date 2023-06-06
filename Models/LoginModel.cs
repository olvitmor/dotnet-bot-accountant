using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace dotnet_bot_accountant.Models;

public class LoginModel
{
    [Required]
    [FromForm(Name = "username")]
    public string Username { get; set; }

    [Required]
    [FromForm(Name = "password")]
    public string Password { get; set; }
}
