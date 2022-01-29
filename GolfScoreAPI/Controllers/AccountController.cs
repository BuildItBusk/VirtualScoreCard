using GolfScoreAPI.Models;
using GolfScoreAPI.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;

namespace GolfScoreAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly JwtTokenOptions jwtSettings;

    public AccountController(JwtTokenOptions jwtSettings)
    {
        this.jwtSettings = jwtSettings ?? throw new ArgumentNullException(nameof(jwtSettings));
    }

    [HttpPost]
    public IActionResult GetToken(string username, string password)
    {
        if (IsValidUser(username, password, out User user))
        { 
            UserToken token = JwtHelpers.GetTokenKey(new UserToken()
            {
                UserName = user.UserName,
                EmailId = user.EmailId,
                GuidId = Guid.NewGuid(),
                Id = user.Id
            }, jwtSettings);

            return Ok(token);
        }
        else
        {
            return BadRequest("Invalid username and/or password.");
        }
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult GetList()
    {
        return Ok();
    }

    private static bool IsValidUser(string username, string password, out User user)
    {
        user = new User { Id = Guid.NewGuid(), UserName = "Admin", EmailId = "busk.soerensen@gmail.com" };
        return true;
    }

}
