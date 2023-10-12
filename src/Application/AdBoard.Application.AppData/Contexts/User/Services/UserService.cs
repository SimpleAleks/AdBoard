using System.Security.Claims;
using AdBoard.Application.AppData.Authorization.Requirements.Operation;
using AdBoard.Application.AppData.Contexts.Advert.Repositories;
using AdBoard.Application.AppData.Contexts.Advert.Services;
using AdBoard.Application.AppData.Helpers.Authentication;
using AdBoard.Contracts.User;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace AdBoard.Application.AppData.Contexts.User.Services;

using User = AdBoard.Domain.User.User;

/// <inheritdoc cref="IUserService"/>
public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    private readonly IAuthorizationService _authorizationService;

    public UserService(
        IUserRepository repository, 
        IHttpContextAccessor httpContextAccessor, 
        IMapper mapper, 
        IAuthorizationService authorizationService)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
        _authorizationService = authorizationService;
    }

    public Task<ShortUserDto[]> GetAll(CancellationToken cancellationToken)
    {
        return _repository.GetAll(cancellationToken);
    }

    public Task<UserDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return _repository.GetById(id, cancellationToken);
    }

    public async Task<UserDto> GetCurrent(CancellationToken cancellationToken)
    {
            var claims = _httpContextAccessor.HttpContext?.User.Claims;
            if (claims is null) throw new ArgumentNullException(nameof(claims));

            var idString = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (idString is null) throw new ArgumentNullException(nameof(idString));

            var id = Guid.Parse(idString);
            var user = await _repository.GetById(id, cancellationToken);
        
            return _mapper.Map<UserDto>(user);
        }

    public async Task<ShortUserDto> Add(CreateUserDto dto, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(dto);
        return await _repository.Add(user, cancellationToken);
    }

    public async Task<ShortUserDto> Update(Guid id, UpdateUserDto dto, CancellationToken cancellationToken)
    {
        var userDto = await _repository.GetById(id, cancellationToken);
        if (userDto is null) throw new ArgumentException("User with id not exist", nameof(id));
        
        var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext!.User, userDto, OperationsList.Update);
        if (!authorizationResult.Succeeded) throw new UnauthorizedAccessException("User hasn't permissions to this user");
        
        var user = _mapper.Map<User>(dto);
        user.Password = AuthenticationHelper.EncryptPassword(dto.Password);
        user.Role = userDto.Role;
        return await _repository.Update(id, user, cancellationToken);
    }

    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        var userDto = await _repository.GetById(id, cancellationToken);
        var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext!.User, userDto, OperationsList.Delete);
        if (!authorizationResult.Succeeded) throw new UnauthorizedAccessException("User hasn't permissions to this user");
        
        return await _repository.Delete(id, cancellationToken);
    }
}