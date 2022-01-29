using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using GolfScoreAPI.Models;

namespace GolfScoreAPI.JwtHelpers;

public static class JwtHelpers
{
    public static IEnumerable<Claim> GetClaims(this UserToken userAccount, Guid id)
    {
        IEnumerable<Claim> claims = new Claim[]
        {
            new Claim("Id", userAccount.Id.ToString()),
            new Claim(ClaimTypes.Name, userAccount.UserName),
            new Claim(ClaimTypes.Email, userAccount.EmailId),
            new Claim(ClaimTypes.NameIdentifier, id.ToString()),
            new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
        };

        return claims;
    }

    public static IEnumerable<Claim> GetClaims(this UserToken userAccount, out Guid id)
    {
        id = Guid.NewGuid();
        return GetClaims(userAccount, id);
    }

    public static UserToken GetTokenKey(UserToken model, JwtSettings jwtSettings)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        var userToken = new UserToken();
        var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
        var id = Guid.Empty;
        var expireTime = DateTime.UtcNow.AddDays(1);

        var JWToken = new JwtSecurityToken(
            issuer: jwtSettings.ValidIssuer, 
            audience: jwtSettings.ValidAudience, 
            claims: GetClaims(model, out id), 
            notBefore: new DateTimeOffset(DateTime.Now).DateTime, 
            expires: new DateTimeOffset(expireTime).DateTime, 
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));

        userToken.Validity = expireTime.TimeOfDay;
        userToken.Token = new JwtSecurityTokenHandler().WriteToken(JWToken);
        userToken.UserName = model.UserName;
        userToken.Id = model.Id;
        userToken.GuidId = id;
        
        return userToken;
    }

}
