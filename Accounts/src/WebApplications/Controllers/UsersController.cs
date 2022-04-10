using Accounts.Application.Users.Commands.CreateUser;
using Accounts.Application.Users.Queries.GetUsersWithPagination;
using Accounts.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.WebApplications.Controllers;

[Authorize]
public class UsersController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<UserViewModel>>> GetTodoItemsWithPagination([FromQuery] GetUsersWithPaginationQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<ActionResult<CreateUserResult>> Create(CreateUserCommand command)
    {
        return await Mediator.Send(command);
    }
}
