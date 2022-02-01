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
    [TestCase("MyPassword", "salt", ExpectedResult = "nFsC5t2MMGPT7qdVTM2w5ufR/X/C9UyoCpunCNTSxNo=")]
    [TestCase("Secret123", "pepper", ExpectedResult = "n6Elp2B2TpytyO3y8RFGURGRijGB/99iUwSYbTPI7UQ=")]
    public string PasswordIsCorrectlyHashed(string password, string salt)
    {        
        int iterations = 10000;
        int keySize = 32;

        return PasswordHelper.HashPassword(password, salt, iterations, keySize);
    }
}
