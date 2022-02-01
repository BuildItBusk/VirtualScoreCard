using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GolfScoreAPI.Authentication;
using GolfScoreAPI.Models;

namespace GolfScoreAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    [HttpPost]
    public IActionResult GetToken(LoginRequest loginRequest)
    {
        if (IsValidUser(loginRequest.Username, 
                        loginRequest.Password, 
                        out UserAccount user))
        {
            string token = TokenHelper.GenerateToken(user);
            return Ok(token);
        }
        else
        {
            return BadRequest("Invalid username and/or password.");
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

    private static bool IsValidUser(string username, string password, out UserAccount user)
    {
        // This is where you would look the user up in the database.
        user = new UserAccount(Id: Guid.NewGuid(), 
                                Username: "Admin", 
                                Email: "busk.soerensen@gmail.com");

        return true;
    }
}
