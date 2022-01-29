﻿using GolfScoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GolfScoreAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly JwtSettings jwtSettings;

    public AccountController(JwtSettings jwtSettings)
    {
        this.jwtSettings = jwtSettings ?? throw new ArgumentNullException(nameof(jwtSettings));
    }

    // Temporary list of users, until database is in place.
    private readonly IEnumerable<User> logins = new List<User>()
    {
        new User()
        {
            Id = Guid.NewGuid(),
            EmailId = "busk.soerensen@gmail.com",
            UserName = "Admin",
            Password = "Admin"
        }
    };

    [HttpPost]
    public IActionResult GetToken(UserLogins userLogins)
    {
        var token = new UserToken();
            
        var valid = logins.Any(l => l.UserName.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
        if (valid)
        {
            var user = logins.FirstOrDefault(l => l.UserName.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
            token = JwtHelpers.JwtHelpers.GetTokenKey(new UserToken()
            {
                UserName = user.UserName,
                EmailId = user.EmailId,
                GuidId = Guid.NewGuid(),
                Id = user.Id
            }, jwtSettings);
        }
        else
        {
            return BadRequest("Wrong password");
        }

        return Ok(token);
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult GetList()
    {
        return Ok(logins);
    }
}