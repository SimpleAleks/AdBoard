namespace AdBoard.Application.AppData.Contexts.Authentication.Exceptions;

public class InvalidLoginException : Exception
{
    public InvalidLoginException() : base() { }
    public InvalidLoginException(string message) : base(message) { }
}