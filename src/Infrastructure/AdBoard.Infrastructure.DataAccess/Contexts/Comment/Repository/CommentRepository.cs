using System.Linq.Expressions;
using AdBoard.Application.AppData.Contexts.Comment.Repositories;
using AdBoard.Application.AppData.Exceptions;
using AdBoard.Contracts.Comment;
using AdBoard.Infrastructure.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AdBoard.Infrastructure.DataAccess.Contexts.Comment.Repository;

using Comment = AdBoard.Domain.Comment.Comment;

/// <inheritdoc cref="ICommentRepository"/>
public class CommentRepository : ICommentRepository
{
    private readonly IRepository<Comment> _repository;
    private readonly IMapper _mapper;

    public CommentRepository(IRepository<Comment> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ShortCommentDto[]> GetAllWhere(Expression<Func<Comment, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _repository.GetAllFiltered(predicate)
            .ProjectTo<ShortCommentDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<CommentDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.GetAllFiltered(c => c.Id == id)
            .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ShortCommentDto> Add(Comment comment, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(comment, cancellationToken);
        var result = (await _repository.GetByIdAsync((Guid)comment.Id!, cancellationToken))!;
        return _mapper.Map<ShortCommentDto>(result);
    }

    public async Task<ShortCommentDto> Update(Comment comment, CancellationToken cancellationToken)
    {
        var existingComment = await _repository.GetByIdAsync((Guid)comment.Id!, cancellationToken);
        if (existingComment is null) throw new ModelWithIdNotFound(nameof(existingComment));
        existingComment.Text = comment.Text;
        await _repository.UpdateAsync(existingComment, cancellationToken);
        return _mapper.Map<ShortCommentDto>(existingComment);
    }

    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        var comment = await _repository.GetByIdAsync(id, cancellationToken);
        if (comment is null) return false;
        await _repository.DeleteAsync(comment, cancellationToken);
        return true;
    }
}