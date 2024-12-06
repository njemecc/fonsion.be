using Fonsion.be.Application.Common.Dtos.Users;
using Fonsion.be.Application.Users.Commands.CreateUser;
using Fonsion.be.Application.Users.Queries.AuthenticateUser;
using Fonsion.be.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fonsion.be.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{

    private readonly IMediator _mediator;
    

    public UserController(UserManager<User> userManager, IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpPost("/register")]

    public async Task<IActionResult> Register([FromBody] CreateUserRequest createUserRequest)
    {
        var command = new CreateUserCommand(createUserRequest);

        var response = await _mediator.Send(command);

        return Ok(response);

    }


    [HttpPost("/login")]
    
    public async Task<IActionResult> Login([FromBody] AuthenticateUserRequest authenticateUserRequest)
    {
        var query = new AuthenticateUserQuery(authenticateUserRequest);

        var response = await _mediator.Send(query);

        return Ok(response);
    }
}