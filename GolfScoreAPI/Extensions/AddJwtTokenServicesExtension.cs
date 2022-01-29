using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using GolfScoreAPI.Models;

namespace GolfScoreAPI.Extensions;

public static class AddJwtTokenServicesExtension
{
    public static void AddJwtTokenServices(this IServiceCollection services, IConfiguration configuration)
    {
        var bindJwtSettings = new JwtSettings();
        configuration.Bind("JsonWebTokenKeys", bindJwtSettings);

        services.AddSingleton(bindJwtSettings);
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
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigningKey)),
                ValidateIssuer = bindJwtSettings.ValidateIssuer,
                ValidIssuer = bindJwtSettings.ValidIssuer,
                ValidateAudience = bindJwtSettings.ValidateAudience,
                ValidAudience = bindJwtSettings.ValidAudience,
                RequireExpirationTime = bindJwtSettings.RequireExpirationTime,
                ValidateLifetime = bindJwtSettings.ValidateLifetime,
                ClockSkew = TimeSpan.FromDays(1)
            };
        });
    }
}
