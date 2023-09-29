using AdBoard.Application.AppData.Contexts.Advert.Repositories;
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
    
    public Task<ShortAdvertDto[]> GetAll(AdvertSearchRequestDto? request,
        CancellationToken cancellationToken)
    {
        var query = _repository.GetAll();

        if (request is not null)
        {
            if (request.Search is not null)
            {
                var searchWords = request.Search.Split(' ');
                query = searchWords.Aggregate(query,
                    (currentQuery, word) => currentQuery.Where(x => x.Name.ToLower().Trim().Contains(word) || x.Description!.ToLower().Trim().Contains(word)));
            }

            if (!(request.ShowNonActive.HasValue && request.ShowNonActive.Value))
            {
                query = query.Where(x => x.IsActive);
            }

            if (request.Category is not null)
            {
                query = query.Where(x => x.CategoryId == request.Category);
            }
        }
        
        return query
            .ProjectTo<ShortAdvertDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<ShortAdvertDto[]> GetAllBySearch(string search, CancellationToken cancellationToken)
    {
        var query = _repository.GetAll();
        var searchWords = search.Split(' ');
        query = searchWords.Aggregate(query,
            (currentQuery, word) => currentQuery.Where(x => x.Name.ToLower().Trim().Contains(word) || x.Description!.ToLower().Trim().Contains(word)));
        return await query
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