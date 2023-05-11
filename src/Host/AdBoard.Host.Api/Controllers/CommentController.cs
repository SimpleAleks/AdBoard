using AdBoard.Application.AppData.Contexts.Comment.Services;
using AdBoard.Application.AppData.Exceptions;
using AdBoard.Contracts;
using AdBoard.Contracts.Advert;
using AdBoard.Contracts.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdBoard.Host.Api.Controllers;

/// <summary>
/// Контроллер для работы с комментариями
/// </summary>
/// <response code="500"> Произошла внутрення ошибка </response>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
[ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    /// <summary>
    /// Контроллер для работы с комментариями
    /// </summary>
    /// <param name="commentService"></param>
    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    /// <summary>
    /// Получить комментарий по id
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="200">Запрос выполнен успешно</response>
    /// <response code="404">Комментарий с таким id не найден</response>
    /// <returns>Модель комментария <see cref="CommentDto"/></returns>
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CommentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _commentService.GetById(id, cancellationToken);
        if (result is null) return NotFound();
        return Ok(result);
    }

    /// <summary>
    /// Обновляет комментарий по модели
    /// </summary>
    /// <param name="id">Идентификатор комментария</param>
    /// <param name="model">Модель</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="200">Запрос успешно выполнен</response>
    /// <response code="400">Модель невалидна</response>
    /// <response code="403">Доступ запрещен</response>
    /// <response code="404">Комментарий не найден</response>
    /// <response code="422">Конфликт бизнес логики</response>
    /// <returns>Модель обновленного комментария <see cref="ShortCommentDto"/></returns>
    [HttpPut("{id:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(ShortCommentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Update(Guid id, UpdateCommentDto model, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _commentService.Update(id, model, cancellationToken);
            return Ok(result);
        }
        catch (ModelWithIdNotFound e)
        {
            return NotFound();
        }
        catch (UnauthorizedAccessException e)
        {
            return Forbid();
        }
    }

    /// <summary>
    /// Удаляет комментарий по ID
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="204">Удаление произведено успешно</response>
    /// <response code="403">Нет доступа</response>
    /// <response code="404">Комментария с таким ID не найдено</response>
    [HttpDelete("{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _commentService.Delete(id, cancellationToken);
            return result ? NoContent() : NotFound();
        }
        catch (UnauthorizedAccessException e)
        {
            return Forbid();
        }
    }
}