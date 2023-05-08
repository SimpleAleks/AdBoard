using AdBoard.Application.AppData.Contexts.Authentication.Exceptions;
using AdBoard.Application.AppData.Contexts.Authentication.Services;
using AdBoard.Contracts;
using AdBoard.Contracts.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdBoard.Host.Api.Controllers;

/// <summary>
/// Контроллер для авторизации и регистрации
/// </summary>
/// <response code="500"> Произошла внутрення ошибка </response>
[ApiController]
[AllowAnonymous]
[Route("Auth")]
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
    /// <response code="400">Модель данных не валидна</response>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>JWT токен</returns>
    [HttpPost("Login")]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(LoginUserDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var loginResult = await _authenticationService.Login(dto, cancellationToken);
            return await Task.Run(() => Ok(loginResult), cancellationToken);
        }
        catch (InvalidLoginDataException e)
        {
            ModelState.AddModelError("errors", "Invalid login or password");
            return BadRequest(ModelState);
        }
    }
    
    /// <summary>
    /// Создаёт пользователя
    /// </summary>
    /// <param name="dto">Модель пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="202">Создание выполнено успешно</response>
    /// <response code="400">Модель данных не валидна</response>
    /// <returns>ID созданного пользователя</returns>
    [HttpPost("Register")]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(CreateUserDto dto, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.Register(dto, cancellationToken);
        return await Task.Run(() => CreatedAtAction(nameof(Register), result) , cancellationToken);
    }
}