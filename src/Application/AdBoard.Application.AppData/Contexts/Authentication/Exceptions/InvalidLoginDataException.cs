namespace AdBoard.Application.AppData.Contexts.Authentication.Exceptions;

/// <summary>
/// Неверный логин или пароль
/// </summary>
public class InvalidLoginDataException : Exception
{
    public InvalidLoginDataException() : base() { }
    public InvalidLoginDataException(string message) : base(message) { }
}