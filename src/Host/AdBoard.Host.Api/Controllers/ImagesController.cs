using AdBoard.Application.AppData.Contexts.Image.Services;
using AdBoard.Contracts;
using AdBoard.Contracts.Image;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdBoard.Host.Api.Controllers;

using Image = AdBoard.Domain.Image.Image;

/// <summary>
/// Контроллер для работы с <see cref="Image"/>
/// </summary>
/// <response code="500">Произошла внутрення ошибка</response> 
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
public class ImagesController : ControllerBase 
{
    private readonly IImageService _service;
    private readonly ILogger<ImagesController> _logger;

    /// <summary>
    /// Инициализирует контроллер <see cref="Image"/>
    /// </summary>
    /// <param name="service">Сервис для работы с изображениями</param>
    /// <param name="logger">Логгер</param>
    public ImagesController(IImageService service, ILogger<ImagesController> logger)
    {
        _service = service;
        _logger = logger;
    }

    /// <summary>
    /// Получить изображение по id
    /// </summary>
    /// <param name="id">Идентификатор изображения</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="200">Запрос выполнен успешно</response>
    /// <response code="404">Изображение с таким id не найдено</response>
    /// <returns>Модель изображения <see cref="ImageDto"/></returns>
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK, "image/jpg")]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogTrace("Request image by id: {Id}", id);
        var result = await _service.GetById(id, cancellationToken);
        if (result is null) return NotFound();
        return File(result.Content, "image/jpg");
    }

    /// <summary>
    /// Создаёт изображение по модели
    /// </summary>
    /// <param name="dto">Модель для создания изображения</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="201">Изображение успешно создано</response>
    /// <response code="400">Модель данных запроса невалидна</response>
    /// <response code="403">Недостаточно прав доступа к изображению</response>
    /// <response code="422">Произошёл конфликт бизнес логики</response>
    /// <returns>Модель созданного изображения <see cref="ShortImageDto"/></returns>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(ShortImageDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create([FromForm] CreateImageDto dto, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogTrace("Request to create image");
            var result = await _service.Add(dto, cancellationToken);
            _logger.LogTrace("Image created with id: {Id}", result.Id);
            return CreatedAtAction(nameof(Create), result);
        }
        catch (UnauthorizedAccessException e)
        {
            return Forbid();
        }
    }

    /// <summary>
    /// Удаляет изображение по ID
    /// </summary>
    /// <param name="id">Идентификатор изображения</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="204">Удаление произведено успешно</response>
    /// <response code="403">Нет доступа</response>
    /// <response code="404">Изображение с таким ID не найден</response>
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