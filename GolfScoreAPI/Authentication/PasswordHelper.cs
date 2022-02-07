using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace GolfScoreAPI.Authentication;

public static class PasswordHelper
{
    private const int keySize = 32;
    private const int iterations = 100000;

    public static string HashPassword(string password, string salt)
    {
        byte[] saltBytes = Encoding.ASCII.GetBytes(salt);

        // derive a 256-bit subkey
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: saltBytes,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: iterations,
            numBytesRequested: keySize));

        return $"{salt}:{hashed}";
    }

    public static bool IsMatch(string password, string hashedPassword)
    {
        string salt = GetSalt(hashedPassword);
        string hashedInput = HashPassword(password, salt);
        return hashedInput == hashedPassword;
    }

    public static string GenerateSalt(int saltLength)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(saltLength);
        return Convert.ToBase64String(salt);
    }

    private static string GetSalt(string hashedPassword)
    {
        string[] passwordSplit = hashedPassword.Split(":");
        return passwordSplit[0];
    }
}
