using Microsoft.AspNetCore.Authorization;
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
    [HttpPost]
    public IActionResult GetToken(string username, string password)
    {
        if (IsValidUser(username, password, out UserAccount user))
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
    [Authorize]
    public IActionResult GetUser()
    {
        // This end point is just used to verify that we are authorized correctly.
        return Ok("Congrats, you are authorized to see this.");
    }

    private static bool IsValidUser(string username, string password, out UserAccount user)
    {
        // This is where you would look the user up in the database.
        user = new UserAccount(Id: Guid.NewGuid(), Username: "Admin", Email: "busk.soerensen@gmail.com");
        return true;
    }

    private static string GenerateToken(UserAccount user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString())
    };

        var header = new JwtHeader(
            new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisKeyMustBeAtLeast16Characters")), SecurityAlgorithms.HmacSha256));

        var payload = new JwtPayload(claims);

        var token = new JwtSecurityToken(header, payload);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private record UserAccount(Guid Id, string Username, string Email);
}
