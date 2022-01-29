using GolfScoreAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(tokenOptions.IssuerSigningKey)),
                ValidateIssuer = tokenOptions.ValidateIssuer,
                ValidIssuer = tokenOptions.ValidIssuer,
                ValidateAudience = tokenOptions.ValidateAudience,
                ValidAudience = tokenOptions.ValidAudience,
                RequireExpirationTime = tokenOptions.RequireExpirationTime,
                ValidateLifetime = tokenOptions.ValidateLifetime,
                ClockSkew = TimeSpan.FromDays(1)
            };
        });
    }
}
