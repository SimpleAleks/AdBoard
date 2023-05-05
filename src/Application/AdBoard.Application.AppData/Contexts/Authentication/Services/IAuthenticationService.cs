using AdBoard.Contracts.User;

namespace AdBoard.Application.AppData.Contexts.Authentication.Services;

/// <summary>
/// Сервис для аутентификации и регистрации пользователей
/// </summary>
public interface IAuthenticationService
{
    /// <summary>
    /// Регистрирует пользователей
    /// </summary>
    /// <param name="dto"> Модель пользователя для создания </param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Идентификатор созданного пользователя</returns>
    Task<Guid> Register(CreateUserDto dto, CancellationToken cancellationToken);
    
    /// <summary>
    /// Возвращает JWT токен пользователя
    /// </summary>
    /// <param name="login">Логин пользователя</param>
    /// <param name="password">Пароль пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>JWT токен</returns>
    Task<string> Login(LoginUserDto dto, CancellationToken cancellationToken);
    
    /// <summary>
    /// Возвращает текущего пользователя
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Текущий пользователь <see cref="UserDto"/></returns>
    Task<UserDto?> GetCurrent(CancellationToken cancellationToken);
}