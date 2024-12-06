using Fonsion.be.Domain.Users;

namespace Fonsion.be.Application.Common.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}