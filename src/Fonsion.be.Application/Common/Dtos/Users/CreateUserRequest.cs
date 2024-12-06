namespace Fonsion.be.Application.Common.Dtos.Users;

public record CreateUserRequest(string FirstName, string LastName, string Email, string Password);
