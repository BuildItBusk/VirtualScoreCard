using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GolfScoreAPI.Authentication;
using GolfScoreAPI.Models;
using GolfScoreAPI.DbContexts;

namespace GolfScoreAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private readonly UserProfileContext _userProfileContext;

    public TokenController(UserProfileContext userProfileContext)
    {
        _userProfileContext = userProfileContext ?? throw new ArgumentNullException(nameof(userProfileContext));
    }

    [HttpPost]
    public IActionResult GetToken(LoginRequest loginRequest)
    {
        if (IsValidUser(loginRequest.Username, 
                        loginRequest.Password, 
                        out UserProfile? user))
        {
            string token = TokenHelper.GenerateToken(user);
            return Ok(token);
        }
        else
        {
            return Unauthorized("Invalid username and/or password.");
        }
    }

    public record LoginRequest(string Username, string Password);

    [HttpGet]
    [Authorize]
    public IActionResult GetUser()
    {
        // This end point is just used to verify that we are authorized correctly.
        return Ok("Congrats, you are authorized to see this.");
    }

    private bool IsValidUser(string username, string password, out UserProfile? user)
    {
        user = _userProfileContext
            .UserProfiles?
            .FirstOrDefault(user => user.UserName == username);
        
        if (user == null)
            return false;

        bool passwordIsValid = PasswordHelper.IsMatch(password, user.Password);

        return passwordIsValid;
    }
}
