using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using GolfScoreAPI.DbContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Logging
builder.Logging.AddAzureWebAppDiagnostics();

// JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
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

string connectionString = builder.Configuration.GetConnectionString("GolfScoreConnectionString");

/*
if (builder.Environment.IsProduction())
{
    string keyVaultUrl = builder.Configuration["KeyVaultUrl"];
    var client = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());
    var secret = await client.GetSecretAsync("DatabaseConnectionString");
    connectionString = secret.Value.ToString() ?? throw new NullReferenceException(nameof(secret));

    System.Diagnostics.Trace.TraceInformation("Running in production - getting connection string from Azure Key Vault...");
}
else
{
    connectionString = builder.Configuration["ConnectionString"];
    System.Diagnostics.Trace.TraceInformation("Running in development - using connection string from local secrets...");
}*/

System.Diagnostics.Trace.TraceInformation(connectionString);

builder.Services.AddDbContext<UserProfileContext>(options =>
                    options.UseSqlServer(connectionString));   

builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
    "v1", new OpenApiInfo
    {
        Title = "Golf Score API",
        Version = "v1"
    });

    // This section allows submitting the token with your request in Swagger.
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    });

    // This section allows submitting the token with your request in Swagger.
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
