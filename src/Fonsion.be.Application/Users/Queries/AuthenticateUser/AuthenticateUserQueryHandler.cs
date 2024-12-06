using ErrorOr;
using Fonsion.be.Application.Common.Dtos.Users;
using Fonsion.be.Application.Common.Interfaces;
using Fonsion.be.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Fonsion.be.Application.Users.Queries.AuthenticateUser;

public class AuthenticateUserQueryHandler : IRequestHandler<AuthenticateUserQuery,ErrorOr<AuthenticateUserResponse>>
{
    
    private readonly SignInManager<User> _signinManager;
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;

    public AuthenticateUserQueryHandler(SignInManager<User> signinManager, UserManager<User> userManager, ITokenService tokenService)
    {
        _signinManager = signinManager;
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<ErrorOr<AuthenticateUserResponse>> Handle(AuthenticateUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.AuthenticateUserRequest.Email, cancellationToken: cancellationToken);
        
        if (user == null) return Error.Unauthorized("Invalid Email!");

        var result = await _signinManager.CheckPasswordSignInAsync(user, request.AuthenticateUserRequest.Password, false);
        
        if (!result.Succeeded) return Error.Unauthorized("Password is not correct");

        return new AuthenticateUserResponse(user.Email, _tokenService.CreateToken(user));

    }
}