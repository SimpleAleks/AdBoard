using AdBoard.Application.AppData.Contexts.Authentication.Services;
using AdBoard.Contracts;
using AdBoard.Contracts.User;
using Microsoft.AspNetCore.Mvc;

namespace AdBoard.Host.Api.Controllers;

/// <summary>
/// Контроллер для авторизации и регистрации
/// </summary>
/// <response code="500"> Произошла внутрення ошибка </response>
[ApiController]
[Route("auth")]
[Produces("application/json")]
[ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
public class AuthorizationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    /// <summary>
    /// Инициализирует контроллер
    /// </summary>
    /// <param name="authenticationService">Сервис аутентификации</param>
    public AuthorizationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    /// <summary>
    /// Аутентифицирует пользователя
    /// </summary>
    /// <param name="dto">Модель для аутентификации</param>
    /// <response code="200">Запрос выполнен успешно</response>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>JWT токен</returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto dto, CancellationToken cancellationToken)
    {
        var loginResult = await _authenticationService.Login(dto, cancellationToken);
        return await Task.Run(() => Ok(loginResult), cancellationToken);
    }
    
    /// <summary>
    /// Создаёт пользователя
    /// </summary>
    /// <param name="dto">Модель пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="202">Создание выполнено успешно</response>
    /// <returns>ID созданного пользователя</returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserDto dto, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.Register(dto, cancellationToken);
        return await Task.Run(() => CreatedAtAction(nameof(Register), result) , cancellationToken);
    }
}