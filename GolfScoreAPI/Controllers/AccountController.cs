using GolfScoreAPI.Models;
using GolfScoreAPI.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            string token = GenerateToken(user);

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

    private static string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.EmailId),
            new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString())
    };

        var header = new JwtHeader(
            new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisKeyMustBeAtLeast16Characters")), SecurityAlgorithms.HmacSha256));

        var payload = new JwtPayload(claims);

        var token = new JwtSecurityToken(header, payload);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
