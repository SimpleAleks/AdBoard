namespace AdBoard.Application.AppData.Contexts.Authentication.Exceptions;

public class InvalidPasswordException : Exception
{
    public InvalidPasswordException() : base() { }
    public InvalidPasswordException(string message) : base(message) { }
}