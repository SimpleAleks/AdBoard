using AdBoard.Application.AppData.Contexts.Advert.Services;
using AdBoard.Application.AppData.Contexts.Comment.Services;
using AdBoard.Application.AppData.Exceptions;
using AdBoard.Contracts;
using AdBoard.Contracts.Advert;
using AdBoard.Contracts.Comment;
using AdBoard.Domain.Advert;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdBoard.Host.Api.Controllers;

/// <summary>
/// Контроллер для работы с объявлениями
/// </summary>
/// <response code="500"> Произошла внутрення ошибка </response>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
public class AdvertController : ControllerBase
{
    private readonly ILogger<AdvertController> _logger;
    private readonly IAdvertService _service;
    private readonly ICommentService _commentService;

    /// <summary>
    /// Инициализирует экземпляр <see cref="AdvertController"/>
    /// </summary>
    /// <param name="logger">Логгер</param>
    /// <param name="service">Сервис для работы с <see cref="Advert"/></param>
    /// <param name="commentService">Сервис дял работы с комментариями</param>
    public AdvertController(ILogger<AdvertController> logger, IAdvertService service, ICommentService commentService)
    {
        _logger = logger;
        _service = service;
        _commentService = commentService;
    }

    /// <summary>
    /// Получить список объявлений
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="200">Запрос выполнен успешно</response>
    /// <returns>Список моделей объявления <see cref="ShortAdvertDto"/></returns>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<ShortAdvertDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var result = await _service.GetAll(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Получить объявление по id
    /// </summary>
    /// <param name="id">Идентификатор объявления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="200">Запрос выполнен успешно</response>
    /// <response code="404">Объявление с таким id не найдено</response>
    /// <returns>Модель объявления <see cref="AdvertDto"/></returns>
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AdvertDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _service.GetById(id, cancellationToken);
        if (result is null) return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Получить список комментариев объявления
    /// </summary>
    /// <param name="id">Идентификатор объявления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="200">Запрос выполнен успешно</response>
    /// <returns>Список моделей комментариев <see cref="ShortCommentDto"/></returns>
    [HttpGet("{id:guid}/Comments")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<ShortCommentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllComments(Guid id, CancellationToken cancellationToken)
    {
        var result = await _commentService.GetAllByAdvertId(id, cancellationToken);
        return Ok(result);
    }
    
    /// <summary>
    /// Создаёт комментарий по модели в объявлении
    /// </summary>
    /// <param name="id">Идентификатор объявления</param>
    /// <param name="dto">Модель для создания комментария</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="201">Успешно создано</response>
    /// <response code="400">Модель данных запроса невалидна</response>
    /// <response code="422">Произошёл конфликт бизнес логики</response>
    /// <returns>Модель созданного комментария <see cref="ShortCommentDto"/></returns>
    [HttpPost("{id:guid}/comments")]
    [Authorize]
    [ProducesResponseType(typeof(ShortCommentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> CreateComment(Guid id, [FromForm] CreateCommentDto dto, CancellationToken cancellationToken)
    {
        var result = await _commentService.Create(id, dto, cancellationToken);
        return CreatedAtAction(nameof(Create), result);
    }
    
    /// <summary>
    /// Создаёт объявление по модели
    /// </summary>
    /// <param name="dto">Модель для создания объявления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="201">Объявление успешно создано</response>
    /// <response code="400">Модель данных запроса невалидна</response>
    /// <response code="422">Произошёл конфликт бизнес логики</response>
    /// <returns>Модель созданного объявления <see cref="ShortAdvertDto"/></returns>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ShortAdvertDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create([FromForm] CreateAdvertDto dto, CancellationToken cancellationToken)
    {
        dto.Images ??= Array.Empty<IFormFile>();
        var result = await _service.Add(dto, cancellationToken);
        return CreatedAtAction(nameof(Create), result);
    }

    /// <summary>
    /// Обновляет объявление по модели
    /// </summary>
    /// <param name="id">Идентификатор обновляемого объявления</param>
    /// <param name="dto">Модель объявления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="200">Запрос успешно выполнен</response>
    /// <response code="400">Модель невалидна</response>
    /// <response code="403">Доступ запрещен</response>
    /// <response code="404">Объявление не найдено</response>
    /// <response code="422">Конфликт бизнес логики</response>
    /// <returns>Модель обновленного объявления <see cref="ShortAdvertDto"/></returns>
    [HttpPut("{id:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(ShortAdvertDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update(Guid id, [FromForm] UpdateAdvertDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.Update(id, dto, cancellationToken);
            return Ok(result);
        }
        catch (UnauthorizedAccessException e)
        {
            return Forbid();
        }
        catch (ModelWithIdNotFound e)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Удаляет объявление по ID
    /// </summary>
    /// <param name="id">Идентификатор объявления</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="204">Удаление произведено успешно</response>
    /// <response code="403">Нет доступа</response>
    /// <response code="404">Объявление с таким ID не найдено</response>
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