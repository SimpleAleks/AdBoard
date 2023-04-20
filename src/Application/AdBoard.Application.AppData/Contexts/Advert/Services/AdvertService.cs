using AdBoard.Application.AppData.Contexts.Advert.Repositories;
using AdBoard.Contracts.Advert;
using AutoMapper;

namespace AdBoard.Application.AppData.Contexts.Advert.Services;

using Advert = AdBoard.Domain.Advert.Advert;

/// <inheritdoc cref="IAdvertService"/>
public class AdvertService : IAdvertService
{
    private readonly IAdvertRepository _repository;
    private readonly IMapper _mapper;

    public AdvertService(IAdvertRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public Task<ShortAdvertDto[]> GetAll(CancellationToken cancellationToken)
    {
        return _repository.GetAll(cancellationToken);
    }

    public Task<AdvertDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return _repository.GetById(id, cancellationToken);
    }

    public Task<ShortAdvertDto> Add(CreateAdvertDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateAdvertDto, Advert>(dto);
        return _repository.Add(entity, cancellationToken);
    }

    public Task<ShortAdvertDto> Update(Guid id, UpdateAdvertDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<UpdateAdvertDto, Advert>(dto);
        return _repository.Update(id, entity, cancellationToken);
    }

    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.Delete(id, cancellationToken);
    }
}