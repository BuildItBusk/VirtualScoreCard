using GolfScoreAPI.Authentication;
using GolfScoreAPI.DbContexts;
using GolfScoreAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace GolfScoreAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserProfileController : ControllerBase
{
    private readonly UserProfileContext userProfileContext;
    private readonly ILogger logger;

    public UserProfileController(UserProfileContext userProfileContext, ILogger<UserProfileController> logger)
    {
        this.userProfileContext = userProfileContext ?? throw new ArgumentNullException(nameof(userProfileContext));
        this.logger = logger;
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

        userProfileContext.Database.EnsureCreated();
        userProfileContext.UserProfiles.Add(user);
        userProfileContext.Credentials.Add(credential);
        userProfileContext.SaveChanges();

        return Ok(user.Id);
    }

    [HttpGet]
    public IActionResult ListUsers()
    {
        logger.LogInformation("Getting all users... (Logger)");
        System.Diagnostics.Trace.TraceInformation("Getting all users... (TraceInformation)");
        System.Diagnostics.Trace.WriteLine("Getting all users... (WriteLine)");
        Console.WriteLine("Getting all users (Console.WriteLine)");

        if (userProfileContext.UserProfiles is null)
            return NotFound();

        List<UserProfile> users = userProfileContext.UserProfiles.ToList();

        if (users == null || !users.Any())
            NotFound("No users in database yet.");

        return Ok(users);
    }

    private bool UserExists(string email)
    {
        return userProfileContext.UserProfiles.Any(x => x.Email == email);
    }
}
