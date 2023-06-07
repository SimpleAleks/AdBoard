using System.Security.Claims;
using AdBoard.Application.AppData.Authorization.Requirements.Operation;
using AdBoard.Application.AppData.Contexts.Advert.Repositories;
using AdBoard.Application.AppData.Exceptions;
using AdBoard.Contracts.Advert;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace AdBoard.Application.AppData.Contexts.Advert.Services;

using Advert = AdBoard.Domain.Advert.Advert;

/// <inheritdoc cref="IAdvertService"/>
public class AdvertService : IAdvertService
{
    private readonly IAdvertRepository _repository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IAuthorizationService _authorizationService;

    public AdvertService(
        IAdvertRepository repository,
        IMapper mapper, 
        IHttpContextAccessor contextAccessor,
        IAuthorizationService authorizationService)
    {
        _repository = repository;
        _mapper = mapper;
        _contextAccessor = contextAccessor;
        _authorizationService = authorizationService;
    }
    
    public Task<ShortAdvertDto[]> GetAll(CancellationToken cancellationToken)
    {
        return _repository.GetAll(cancellationToken);
    }

    public async Task<ShortAdvertDto[]> GetAllBySearch(string search, CancellationToken cancellationToken)
    {
        var trimSearch = search.ToLower().Trim();
        var result = await _repository.GetAllBySearch(trimSearch, cancellationToken);
        return result;
    }

    public Task<AdvertDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return _repository.GetById(id, cancellationToken);
    }

    public Task<ShortAdvertDto> Add(CreateAdvertDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateAdvertDto, Advert>(dto);
        var stringUserId = _contextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        if (stringUserId is null) throw new ArgumentException(nameof(stringUserId));
        entity.UserId = Guid.Parse(stringUserId);
        return _repository.Add(entity, cancellationToken);
    }

    public async Task<ShortAdvertDto> Update(Guid id, UpdateAdvertDto dto, CancellationToken cancellationToken)
    {
        
        var existEntity = await GetById(id, cancellationToken);
        if (existEntity is null) throw new ModelWithIdNotFound($"Advert with {id} not found");
        
        var authorizationResult = await _authorizationService.AuthorizeAsync(_contextAccessor.HttpContext!.User, existEntity, OperationsList.Update);
        if (!authorizationResult.Succeeded) throw new UnauthorizedAccessException("User hasn't permissions to this advert");
        
        var entity = _mapper.Map<UpdateAdvertDto, Advert>(dto);
        entity.UserId = existEntity.User.Id;
        
        return await _repository.Update(id, entity, cancellationToken);
    }

    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        var existEntity = await GetById(id, cancellationToken);
        var authorizationResult = await _authorizationService.AuthorizeAsync(_contextAccessor.HttpContext!.User, existEntity, OperationsList.Delete);
        if (!authorizationResult.Succeeded) throw new UnauthorizedAccessException("User hasn't permissions to this advert");
        
        return await _repository.Delete(id, cancellationToken);
    }
}