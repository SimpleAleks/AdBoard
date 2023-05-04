﻿using AdBoard.Application.AppData.Contexts.Advert.Repositories;
using AdBoard.Contracts.Advert;
using AdBoard.Infrastructure.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AdBoard.Infrastructure.DataAccess.Contexts.Advert.Repository;

using Advert = AdBoard.Domain.Advert.Advert;

/// <inheritdoc cref="IAdvertRepository"/>
public class AdvertRepository : IAdvertRepository
{
    private readonly IRepository<Advert> _repository;
    private readonly IMapper _mapper;

    public AdvertRepository(IRepository<Advert> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public Task<ShortAdvertDto[]> GetAll(CancellationToken cancellationToken)
    {
        return _repository.GetAll()
            .ProjectTo<ShortAdvertDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }

    public Task<AdvertDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return _repository.GetAll()
            .Where(x => x.Id == id)
            .ProjectTo<AdvertDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ShortAdvertDto> Add(Advert advert, CancellationToken cancellationToken)
    {
        advert.Created = DateTime.UtcNow;
        await _repository.AddAsync(advert, cancellationToken);
        return _mapper.Map<Advert, ShortAdvertDto>(advert);
    }

    public async Task<ShortAdvertDto> Update(Guid id, Advert advert, CancellationToken cancellationToken)
    {
        advert.Id = id;
        var dbAdvertImages = await _repository.GetAll()
            .Where(x => x.Id == id)
            .Include(x => x.Images)
            .Select(x => x.Images)
            .FirstOrDefaultAsync(cancellationToken);
        advert.Images = dbAdvertImages ?? Array.Empty<Domain.Image.Image>();
        await _repository.UpdateAsync(advert, cancellationToken);
        return _mapper.Map<Advert, ShortAdvertDto>(advert);
    }

    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity is null) return false;
        await _repository.DeleteAsync(entity, cancellationToken);
        return true;
    }
}