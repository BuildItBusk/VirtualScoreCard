using GolfScoreAPI.Authentication;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests;

[TestFixture]
public class PasswordHelperTests
{
    [Test]
    [TestCase("MyPassword", "salt", ExpectedResult = "salt:HTGDtkzt81QKICbSdImHig1rpQM4TIDdEUzTtPTm8a0=")]
    [TestCase("Secret123", "pepper", ExpectedResult = "pepper:jmYMLmQN78hfw8SZErfrRGbMy3HtwscetAROD8U6M7g=")]
    public string PasswordIsCorrectlyHashed(string password, string salt)
    {        
        return PasswordHelper.HashPassword(password, salt);
    }

    [Test]
    [TestCase("MyPassword", "salt:HTGDtkzt81QKICbSdImHig1rpQM4TIDdEUzTtPTm8a0=", ExpectedResult = true)]
    [TestCase("Secret123", "pepper:jmYMLmQN78hfw8SZErfrRGbMy3HtwscetAROD8U6M7g=", ExpectedResult = true)]
    [TestCase("AnotherPassword", "pepper:jmYMLmQN78hfw8SZErfrRGbMy3HtwscetAROD8U6M7g=", ExpectedResult = false)]
    public bool PasswordIsCorrectlyMatched(string password, string hashedPassword)
    {
        return PasswordHelper.IsMatch(password, hashedPassword);
    }
}
