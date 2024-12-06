using ErrorOr;
using Fonsion.be.Application.Common.Dtos.Users;
using Fonsion.be.Application.Common.Interfaces;
using Fonsion.be.Application.Common.Mappers;
using Fonsion.be.Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Fonsion.be.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand,ErrorOr<CreateUserResponse>>
{
    
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private IUnitOfWork _unitOfWork;

    public  CreateUserCommandHandler(UserManager<User> userManager, ITokenService tokenService, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<CreateUserResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {

            var user = request.CreateUserRequest.FromCreateRequestToModel();

            var createdUser = await _userManager.CreateAsync(user, request.CreateUserRequest.Password);

            if (createdUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "Guest");

                await _unitOfWork.CommitChangesAsync();

                if (roleResult.Succeeded)
                {
                    return new CreateUserResponse(user.Email, _tokenService.CreateToken(user));

                }
                else
                {
                    foreach (var error in roleResult.Errors)
                    {
                        return Error.Conflict($"Role assignment failed: {error.Description}");
                    }

                    return Error.Conflict("Role has not being created succesfully");

                }
            }
            else
            {
                foreach (var error in createdUser.Errors)
                {
                    return Error.Conflict($"User assignment failed: {error.Description}");
                }

                return Error.Conflict("User is not created Succesfully");
            }

        }
        catch (Exception e)
        {
            return Error.Failure("An unexpextec error occured");
        }
    }
}