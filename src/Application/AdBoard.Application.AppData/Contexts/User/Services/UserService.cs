using AdBoard.Application.AppData.Contexts.Advert.Repositories;
using AdBoard.Application.AppData.Contexts.Advert.Services;
using AdBoard.Contracts.User;
using AutoMapper;

namespace AdBoard.Application.AppData.Contexts.User.Services;

using User = AdBoard.Domain.User.User;

/// <inheritdoc cref="IUserService"/>
public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public Task<ShortUserDto[]> GetAll(CancellationToken cancellationToken)
    {
        return _repository.GetAll(cancellationToken);
    }

    public Task<UserDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return _repository.GetById(id, cancellationToken);
    }

    public async Task<ShortUserDto> Add(CreateUserDto dto, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(dto);
        return await _repository.Add(user, cancellationToken);
    }

    public async Task<ShortUserDto> Update(Guid id, UpdateUserDto dto, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(dto);
        return await _repository.Update(id, user, cancellationToken);
    }

    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.Delete(id, cancellationToken);
    }
}