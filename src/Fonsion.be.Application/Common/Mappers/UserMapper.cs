using Fonsion.be.Application.Common.Dtos.Users;
using Fonsion.be.Domain.Users;

namespace Fonsion.be.Application.Common.Mappers;

public static class UserMapper
{
    public static User FromCreateRequestToModel(this CreateUserRequest createUserRequest)
    {
        return new User
        {
            FirstName = createUserRequest.FirstName,
            LastName = createUserRequest.LastName,
            Email = createUserRequest.Email,
            UserName = createUserRequest.Email
        };
    }
}