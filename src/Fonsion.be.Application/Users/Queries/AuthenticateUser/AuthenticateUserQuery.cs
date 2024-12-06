using ErrorOr;
using Fonsion.be.Application.Common.Dtos.Users;
using Fonsion.be.Contracts.Rooms;
using MediatR;

namespace Fonsion.be.Application.Users.Queries.AuthenticateUser;

public record AuthenticateUserQuery(AuthenticateUserRequest AuthenticateUserRequest) : IRequest<ErrorOr<AuthenticateUserResponse>>;