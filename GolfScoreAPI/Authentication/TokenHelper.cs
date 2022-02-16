using GolfScoreAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GolfScoreAPI.Authentication;

public static class TokenHelper
{
    public static string GenerateToken(UserProfile user)
    {
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.Exp,
            new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString())
};

        var header = new JwtHeader(
            new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("ThisKeyMustBeAtLeast16Characters")),
                    SecurityAlgorithms.HmacSha256));

        var payload = new JwtPayload(claims);

        var token = new JwtSecurityToken(header, payload);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
