using AdBoard.Application.AppData.Contexts.Image.Repositories;
using AdBoard.Contracts.Image;
using AdBoard.Infrastructure.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AdBoard.Infrastructure.DataAccess.Contexts.Image.Repository;

using Image = AdBoard.Domain.Image.Image;

/// <inheritdoc cref="IImagesRepository"/>
public class ImageRepository : IImagesRepository
{
    private readonly IRepository<Image> _repository;
    private readonly IMapper _mapper;
    
    /// <summary>
    /// Инициализирует репозиторий изображений
    /// </summary>
    /// <param name="repository">"Глупый" репозиторий</param>
    /// <param name="mapper">Маппер</param>
    public ImageRepository(IRepository<Image> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public Task<ShortImageDto[]> GetAll(CancellationToken cancellationToken)
    {
        return _repository.GetAll()
            .ProjectTo<ShortImageDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }

    public Task<ImageDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return _repository.GetAll()
            .Where(i => i.Id == id)
            .ProjectTo<ImageDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ShortImageDto> Add(Image image, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(image, cancellationToken);
        return _mapper.Map<ShortImageDto>(image);
    }

    public async Task<ShortImageDto> Update(Guid id, Image image, CancellationToken cancellationToken)
    {
        image.Id = id;
        await _repository.UpdateAsync(image, cancellationToken);
        return _mapper.Map<ShortImageDto>(image);
    }

    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        var image = await _repository.GetByIdAsync(id, cancellationToken);
        if (image is null) return false;
        await _repository.DeleteAsync(image, cancellationToken);
        return true;
    }
}