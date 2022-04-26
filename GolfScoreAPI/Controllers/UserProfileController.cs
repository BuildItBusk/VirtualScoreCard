using GolfScoreAPI.Authentication;
using GolfScoreAPI.DbContexts;
using GolfScoreAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GolfScoreAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserProfileController : ControllerBase
{
    private readonly UserProfileContext _userProfileContext;

    public UserProfileController(UserProfileContext userProfileContext)
    {
        _userProfileContext = userProfileContext ?? throw new ArgumentNullException(nameof(userProfileContext));
    }

    [HttpPost]
    public IActionResult CreateUser(UserProfileDto userDto)
    {
        if (UserExists(userDto.Email))
            return Conflict("A user with that e-mail address already exists.");

        var salt = PasswordHelper.GenerateSalt(16);
        var password = PasswordHelper.HashPassword(userDto.Password, salt);
                
        var user = new UserProfile(userDto.Username, userDto.Email);
        var credential = new Credential(user.Id, user.Username, password);

        _userProfileContext.Database.EnsureCreated();
        _userProfileContext.UserProfiles.Add(user);
        _userProfileContext.Credentials.Add(credential);
        _userProfileContext.SaveChanges();

        return Ok(user.Id);
    }

    [HttpGet]
    public IActionResult ListUsers()
    {
        System.Diagnostics.Trace.TraceInformation("Getting all users...");

        if (_userProfileContext.UserProfiles is null)
            return NotFound();

        List<UserProfile> users = _userProfileContext.UserProfiles.ToList();

        if (users == null || !users.Any())
            NotFound("No users in database yet.");

        return Ok(users);
    }

    private bool UserExists(string email)
    {
        return _userProfileContext.UserProfiles.Any(x => x.Email == email);
    }
}
