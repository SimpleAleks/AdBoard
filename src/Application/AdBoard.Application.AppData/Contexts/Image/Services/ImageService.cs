using AdBoard.Application.AppData.Contexts.Image.Repositories;
using AdBoard.Contracts.Image;
using AutoMapper;

namespace AdBoard.Application.AppData.Contexts.Image.Services;

using Image = AdBoard.Domain.Image.Image;

/// <inheritdoc cref="IImageService"/>
public class ImageService : IImageService
{
    private readonly IImagesRepository _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует экземпляр сервиса изображений
    /// </summary>
    /// <param name="repository">Репозиторий изображений</param>
    /// <param name="mapper">Маппер</param>
    public ImageService(IImagesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public Task<ShortImageDto[]> GetAll(CancellationToken cancellationToken)
    {
        return _repository.GetAll(cancellationToken);
    }

    public Task<ImageDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return _repository.GetById(id, cancellationToken);
    }

    public async Task<ShortImageDto> Add(CreateImageDto dto, CancellationToken cancellationToken)
    {
        var image = _mapper.Map<Image>(dto);
        return await _repository.Add(image, cancellationToken);
    }

    public async Task<ShortImageDto> Update(Guid id, UpdateImageDto dto, CancellationToken cancellationToken)
    {
        var image = _mapper.Map<Image>(dto);
        return await _repository.Update(id, image, cancellationToken);
    }

    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.Delete(id, cancellationToken);
    }
}