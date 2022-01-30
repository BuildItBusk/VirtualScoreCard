using GolfScoreAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GolfScoreAPI.Authentication;

public static class AuthenticationExtensions
{
    public static void AddJwtBearer(this IServiceCollection services, IConfiguration configuration)
    {
        var tokenOptions = new JwtTokenOptions();

        configuration.Bind("JwtTokenOptions", tokenOptions);        

        services.AddSingleton(tokenOptions);
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "JwtBearer";
            options.DefaultChallengeScheme = "JwtBearer";
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisKeyMustBeAtLeast16Characters")),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(5)
            };
        });
    }
}
