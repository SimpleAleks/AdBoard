namespace AdBoard.Application.AppData.Exceptions;

/// <summary>
/// Модель с таким Id не найдена
/// </summary>
public class ModelWithIdNotFound : Exception
{
    public ModelWithIdNotFound() : base() {}
    public ModelWithIdNotFound(string message) : base(message) {}
}