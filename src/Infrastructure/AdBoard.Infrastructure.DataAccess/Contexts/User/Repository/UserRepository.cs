using System.Linq.Expressions;
using AdBoard.Application.AppData.Contexts.Advert.Repositories;
using AdBoard.Contracts.Advert;
using AdBoard.Contracts.User;
using AdBoard.Infrastructure.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AdBoard.Infrastructure.DataAccess.Contexts.User.Repository;

using User = AdBoard.Domain.User.User;

/// <inheritdoc cref="IUserRepository"/>
public class UserRepository : IUserRepository
{
    private readonly IRepository<User> _repository;
    private readonly IMapper _mapper;

    public UserRepository(IRepository<User> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public Task<ShortUserDto[]> GetAll(CancellationToken cancellationToken)
    {
        return _repository
            .GetAll()
            .ProjectTo<ShortUserDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }

    public Task<UserDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return _repository.GetAll()
            .Where(x => x.Id == id)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<User?> FindWhere(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _repository.GetAllFiltered(predicate).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ShortUserDto> Add(User user, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(user, cancellationToken);
        return _mapper.Map<ShortUserDto>(user);
    }

    public async Task<ShortUserDto> Update(Guid id, User user, CancellationToken cancellationToken)
    {
        user.Id = id;
        await _repository.UpdateAsync(user, cancellationToken);
        return _mapper.Map<ShortUserDto>(user);
    }

    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        var advert = await _repository.GetByIdAsync(id, cancellationToken);
        if (advert is null) return false;
        await _repository.DeleteAsync(advert, cancellationToken);
        return true;
    }
}