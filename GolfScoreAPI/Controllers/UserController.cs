using GolfScoreAPI.Authentication;
using GolfScoreAPI.DbContexts;
using GolfScoreAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GolfScoreAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly AccountContext _accountContext;

    public UserController(AccountContext accountContext)
    {
        _accountContext = accountContext ?? throw new ArgumentNullException(nameof(accountContext));
    }

    [HttpPost]
    public IActionResult CreateUser(Account user)
    {
        if (_accountContext.Accounts == null)
            throw new InvalidOperationException("Accounts cannot be null.");

        _accountContext.Accounts.Add(user);
        _accountContext.SaveChanges();

        return Ok(user);
    }

    [HttpGet]
    public IActionResult ListUsers()
    {
        if (_accountContext.Accounts is null)
            return NotFound();

        List<Account> users = _accountContext.Accounts.ToList();
        return Ok(users);
    }
}
