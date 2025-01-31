using Domain.Exceptions;

namespace Domain.ValueObjects.Exceptions;

public class InvalidPasswordException(string password)
    : DomainException(string.Format(DefaultInvalidPasswordMessageTemplate, password))
{
    private const string DefaultInvalidPasswordMessageTemplate = "The password {0} is invalid";
}