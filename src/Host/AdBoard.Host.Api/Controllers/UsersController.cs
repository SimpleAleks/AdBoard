using AdBoard.Application.AppData.Contexts.Advert.Services;
using AdBoard.Contracts;
using AdBoard.Contracts.User;
using AdBoard.Domain.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdBoard.Host.Api.Controllers;

/// <summary>
/// Контроллер для работы с <see cref="User"/>
/// </summary>
/// <response code="500">Произошла внутрення ошибка</response> 
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;
    private readonly ILogger<UsersController> _logger;

    /// <summary>
    /// Инициализирует контроллер <see cref="User"/>
    /// </summary>
    /// <param name="service">Сервис для работы с пользователями</param>
    /// <param name="logger">Логгер</param>
    public UsersController(IUserService service, ILogger<UsersController> logger)
    {
        _service = service;
        _logger = logger;
    }

    /// <summary>
    /// Вернуть всех пользователей
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="200">Запрос выполнен успешно</response>
    /// <response code="403">Доступ запрещен</response>
    /// <returns>Список пользователи</returns>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(IEnumerable<ShortUserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IEnumerable<ShortUserDto>), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogTrace("Request all users");
        var result = await _service.GetAll(cancellationToken);
        return Ok(result);
    }
    
    /// <summary>
    /// Получить пользователя по id
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="200">Запрос выполнен успешно</response>
    /// <response code="404">Пользователь с таким id не найден</response>
    /// <returns>Модель пользователя <see cref="UserDto"/></returns>
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogTrace("Request user by id: {Id}", id);
        var result = await _service.GetById(id, cancellationToken);
        if (result is null) return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Получить текущего пользователя
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Запрос выполнен успешно</response>
    /// <response code="403">Доступ запрещен</response>
    /// <returns>Текущий пользователь</returns>
    [HttpGet("current")]
    [Authorize]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
    {
        var result = await _service.GetCurrent(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Обновляет пользователя по модели
    /// </summary>
    /// <param name="id">Идентификатор обновляемого пользователя</param>
    /// <param name="dto">Модель пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="200">Запрос успешно выполнен</response>
    /// <response code="400">Модель невалидна</response>
    /// <response code="403">Доступ запрещен</response>
    /// <response code="404">Пользователь не найден</response>
    /// <response code="422">Конфликт бизнес логики</response>
    /// <returns>Модель обновленного пользователя <see cref="ShortUserDto"/></returns>
    [HttpPut("{id:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(ShortUserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update(Guid id, [FromForm] UpdateUserDto dto, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogTrace("Request to update user by id: {Id}", id);
            var result = await _service.Update(id, dto, cancellationToken);
            return Ok(result);
        }
        catch (UnauthorizedAccessException e)
        {
            return Forbid();
        }
        catch (ArgumentException e)
        {
            if (e.ParamName == "id")
            {
                ModelState.AddModelError("Id", e.Message);
            }

            return BadRequest(ModelState);
        }
    }

    /// <summary>
    /// Удаляет пользователя по ID
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="204">Удаление произведено успешно</response>
    /// <response code="403">Нет доступа</response>
    /// <response code="404">Пользователь с таким ID не найден</response>
    [HttpDelete("{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var isRemoved = await _service.Delete(id, cancellationToken);
            if (isRemoved) return NoContent();
            return NotFound();
        }
        catch (UnauthorizedAccessException e)
        {
            return Forbid();
        }
    }
}