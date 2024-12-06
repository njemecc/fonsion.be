using ErrorOr;
using Fonsion.be.Application.Common.Dtos.Users;
using Fonsion.be.Contracts.Rooms;
using MediatR;

namespace Fonsion.be.Application.Users.Commands.CreateUser;

public record CreateUserCommand(CreateUserRequest CreateUserRequest) : IRequest<ErrorOr<CreateUserResponse>>;