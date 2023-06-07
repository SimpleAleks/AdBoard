using System.Security.Claims;
using AdBoard.Application.AppData.Authorization.Requirements.Operation;
using AdBoard.Application.AppData.Contexts.Comment.Repositories;
using AdBoard.Application.AppData.Exceptions;
using AdBoard.Contracts.Comment;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace AdBoard.Application.AppData.Contexts.Comment.Services;

using Comment = AdBoard.Domain.Comment.Comment;

/// <inheritdoc cref="ICommentService"/>
public class CommentService : ICommentService
{
    private readonly ICommentRepository _repository;
    private readonly IMapper _mapper;
    private readonly HttpContext _httpContext;
    private readonly IAuthorizationService _authorizationService;

    public CommentService(
        ICommentRepository repository, 
        IMapper mapper, 
        IHttpContextAccessor httpContextAccessor,
        IAuthorizationService authorizationService)
    {
        _repository = repository;
        _mapper = mapper;
        _httpContext = httpContextAccessor.HttpContext!;
        _authorizationService = authorizationService;
    }
    
    public async Task<ShortCommentDto[]> GetAllByAdvertId(Guid advertId, CancellationToken cancellationToken)
    {
        return await _repository.GetAllWhere(c => c.AdvertId == advertId, cancellationToken);
    }

    public async Task<CommentDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.GetById(id, cancellationToken);
    }

    public async Task<ShortCommentDto> Create(Guid advertId, CreateCommentDto dto, CancellationToken cancellationToken)
    {
        var comment = _mapper.Map<Comment>(dto);
        
        comment.AdvertId = advertId;
        comment.Created = DateTime.UtcNow;
        comment.UserId = Guid.Parse(_httpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
        
        return await _repository.Add(comment, cancellationToken);
    }

    public async Task<ShortCommentDto> Update(Guid id, UpdateCommentDto dto, CancellationToken cancellationToken)
    {
        var existComment = await _repository.GetById(id, cancellationToken);
        if (existComment is null) throw new ModelWithIdNotFound();
        var authResult = await _authorizationService.AuthorizeAsync(_httpContext.User, existComment,
            OperationsList.Update);
        if (!authResult.Succeeded) throw new UnauthorizedAccessException();
        
        var comment = _mapper.Map<Comment>(dto);
        comment.Id = id;

        return await _repository.Update(comment, cancellationToken);
    }

    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        var existComment = await _repository.GetById(id, cancellationToken);
        if (existComment is null) throw new ModelWithIdNotFound();
        var authResult = await _authorizationService.AuthorizeAsync(_httpContext.User, existComment,
            OperationsList.Delete);
        if (!authResult.Succeeded) throw new UnauthorizedAccessException();
        
        return await _repository.Delete(id, cancellationToken);
    }
}