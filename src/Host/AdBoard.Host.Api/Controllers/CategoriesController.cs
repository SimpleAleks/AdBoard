using AdBoard.Application.AppData.Contexts.Category.Services;
using AdBoard.Contracts;
using AdBoard.Contracts.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdBoard.Host.Api.Controllers;

/// <summary>
/// Контроллер для работы с категориями
/// </summary>
/// <response code="500">Произошла внутрення ошибка</response>
[ApiController]
[Authorize(Roles = "Admin")]
[Route("[controller]")]
[Produces("application/json")]
[ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;
    private readonly ILogger<CategoriesController> _logger;

    /// <summary>
    /// Инициализирует контроллер
    /// </summary>
    /// <param name="service">Сервис по работе с категориями</param>
    /// <param name="logger">Логгер</param>
    public CategoriesController(ICategoryService service, ILogger<CategoriesController> logger)
    {
        _service = service;
        _logger = logger;
    }
    
    /// <summary>
    /// Вернуть все категории
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="200">Запрос выполнен успешно</response>
    /// <returns>Список категорий</returns>
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<ShortCategoryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        _logger.LogTrace("Request all categories");
        var result = await _service.GetAll(cancellationToken);
        return Ok(result);
    }
    
    /// <summary>
    /// Получить категорию по id
    /// </summary>
    /// <param name="id">Идентификатор категории</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="200">Запрос выполнен успешно</response>
    /// <response code="404">Категория с таким id не найдена</response>
    /// <returns>Модель категории <see cref="CategoryDto"/></returns>
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogTrace("Request category by id: {Id}", id);
        var result = await _service.GetById(id, cancellationToken);
        if (result is null) return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Создаёт категорию по модели
    /// </summary>
    /// <param name="dto">Модель для создания категории</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="201">Категория успешно создана</response>
    /// <response code="400">Модель данных запроса невалидна</response>
    /// <response code="403">Недостаточно прав для создания категории</response>
    /// <response code="422">Произошёл конфликт бизнес логики</response>
    /// <returns>Модель созданной категории <see cref="ShortCategoryDto"/></returns>
    [HttpPost]
    [ProducesResponseType(typeof(ShortCategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Create([FromForm] CreateCategoryDto dto, CancellationToken cancellationToken)
    {
        _logger.LogTrace("Request to create category");
        var result = await _service.Add(dto, cancellationToken);
        _logger.LogTrace("Category created with id: {Id}", result.Id);
        return CreatedAtAction(nameof(Create), result);
    }

    /// <summary>
    /// Импортирует категории из JSON списка.
    /// </summary>
    /// <param name="importCategories">Коллекция категорий.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <response code="200">Категории успешно импортированы.</response>
    /// <response code="422">Произошёл конфликт бизнес логики.</response>
    [HttpPost("import")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Import(IEnumerable<ImportCategoryDto> importCategories, CancellationToken cancellationToken)
    {
        try
        {
            await _service.ImportAsync(importCategories, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Ошибка при импорте категорий из JSON");
            return UnprocessableEntity();
        }

        return Ok();
    }

    /// <summary>
    /// Обновляет категорию по модели
    /// </summary>
    /// <param name="id">Идентификатор обновляемой категории</param>
    /// <param name="dto">Модель категории</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="200">Запрос успешно выполнен</response>
    /// <response code="400">Модель невалидна</response>
    /// <response code="403">Доступ запрещен</response>
    /// <response code="404">Категория не найдена</response>
    /// <response code="422">Конфликт бизнес логики</response>
    /// <returns>Модель обновленной категории <see cref="ShortCategoryDto"/></returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ShortCategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update(Guid id, [FromForm] UpdateCategoryDto dto, CancellationToken cancellationToken)
    {
        _logger.LogTrace("Request to update category by id: {Id}", id);
        var result = await _service.Update(id, dto, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Удаляет категорию по ID
    /// </summary>
    /// <param name="id">Идентификатор категории</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="204">Удаление произведено успешно</response>
    /// <response code="403">Нет доступа</response>
    /// <response code="404">Категория с таким ID не найдена</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var isRemoved = await _service.Delete(id, cancellationToken);
        if (isRemoved) return NoContent();
        return NotFound();
    }
}