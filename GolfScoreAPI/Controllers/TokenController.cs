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
                        out UserProfileDto? user))
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

    private static bool IsValidUser(string username, string password, out UserProfileDto? user)
    {
        int iterations = 10000;
        int keySize = 32;

        user = Users.FirstOrDefault(user => user.Username == username);
        
        if (user == null)
            return false;

        bool passwordIsValid = PasswordHelper.IsMatch(password, user.Password, iterations, keySize);

        return passwordIsValid;
    }

    private static readonly List<UserProfileDto> Users = new()
    {
        new UserProfileDto(Username: "Admin", Email: "busk.soerensen@gmail.com", Password: "salt:nFsC5t2MMGPT7qdVTM2w5ufR/X/C9UyoCpunCNTSxNo="),
        new UserProfileDto(Username: "NotAdmin", Email: "test@test.dk", Password: "pepper:n6Elp2B2TpytyO3y8RFGURGRijGB/99iUwSYbTPI7UQ=")
    };
}
