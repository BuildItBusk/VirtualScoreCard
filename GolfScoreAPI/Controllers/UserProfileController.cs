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
        var salt = PasswordHelper.GenerateSalt(16);
        var password = PasswordHelper.HashPassword(userDto.Password, salt);
                
        var user = new UserProfile(userDto.Username, userDto.Email);
        var credential = new Credential(user.Id, user.Username, password);

        _userProfileContext.UserProfiles.Add(user);
        _userProfileContext.Credentials.Add(credential);
        _userProfileContext.SaveChanges();

        return Ok(userDto);
    }

    [HttpGet]
    public IActionResult ListUsers()
    {
        if (_userProfileContext.UserProfiles is null)
            return NotFound();

        List<UserProfile> users = _userProfileContext.UserProfiles.ToList();
        return Ok(users);
    }
}
