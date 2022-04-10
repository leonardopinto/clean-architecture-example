using Accounts.Application.Login.Commands;
using Accounts.WebApplications.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace WebApplications.Controllers;

public class LoginController : ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<LoginResult>> CreateAsync(LoginCommand command)
    {
        var result = await Mediator.Send(command);
        if (!result.Succeeded)
            return Unauthorized();

        return Accepted(result);
    }
}
