using GolfScoreAPI.Authentication;
using GolfScoreAPI.DbContexts;
using GolfScoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        Credential? credential = GetCredentialIfValid(loginRequest.Username, loginRequest.Password);

        if (credential is not null)
        {
            var user = GetUserById(credential.UserId);
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
    public IActionResult VerifyAuthorized()
    {
        // This end point is just used to verify that we are authorized correctly.
        return Ok("Congrats, you are authorized to see this.");
    }

    private Credential? GetCredentialIfValid(string username, string password)
    {
        var credential = _userProfileContext
                        .Credentials
                        .FirstOrDefault(credential => credential.Username == username);

        if (credential == null)
            return null;

        bool passwordIsValid = PasswordHelper.IsMatch(password, credential.Password);

        return passwordIsValid ? credential : null;
    }

    private UserProfile GetUserById(Guid userId)
    {
        return _userProfileContext
               .UserProfiles
               .First(user => user.Id == userId);
    }
}
