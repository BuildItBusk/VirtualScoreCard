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
        var user = LookupUser(username, password);
        if (user is null)
            return BadRequest("Incorrect username or password.");

        UserToken token = JwtHelpers.GetTokenKey(new UserToken()
        {
            UserName = user.UserName,
            EmailId = user.EmailId,
            GuidId = Guid.NewGuid(),
            Id = user.Id
        }, jwtSettings);

        return Ok(token);
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult GetList()
    {
        return Ok(logins);
    }

    private User? LookupUser(string username, string password)
    {
        if (username is null)
            throw new ArgumentNullException(nameof(username));

        if (password is null)
            throw new ArgumentNullException(nameof(password));

        return logins.FirstOrDefault(
            l => l.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) && l.Password == password);
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
}
