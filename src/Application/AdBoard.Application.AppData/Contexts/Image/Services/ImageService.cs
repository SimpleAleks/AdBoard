using AdBoard.Application.AppData.Authorization.Requirements.Operation;
using AdBoard.Application.AppData.Contexts.Advert.Services;
using AdBoard.Application.AppData.Contexts.Image.Repositories;
using AdBoard.Contracts.Image;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace AdBoard.Application.AppData.Contexts.Image.Services;

using Image = AdBoard.Domain.Image.Image;

/// <inheritdoc cref="IImageService"/>
public class ImageService : IImageService
{
    private readonly IImagesRepository _repository;
    private readonly IMapper _mapper;
    private readonly IAdvertService _advertService;
    private readonly IAuthorizationService _authorizationService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Инициализирует экземпляр сервиса изображений
    /// </summary>
    /// <param name="repository">Репозиторий изображений</param>
    /// <param name="mapper">Маппер</param>
    /// <param name="advertService">Сервис для работы с объявлениями</param>
    /// <param name="authorizationService">Сервис проверки авторизации</param>
    /// <param name="httpContextAccessor">Дает доступ к <see cref="HttpContext"/></param>
    public ImageService(
        IImagesRepository repository, 
        IMapper mapper, 
        IAdvertService advertService,
        IAuthorizationService authorizationService,
        IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _mapper = mapper;
        _advertService = advertService;
        _authorizationService = authorizationService;
        _httpContextAccessor = httpContextAccessor;
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
        
        var existEntity = await _advertService.GetById((Guid)dto.AdvertId!, cancellationToken);
        var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext!.User, existEntity, OperationsList.Update);
        if (!authorizationResult.Succeeded) throw new UnauthorizedAccessException("User hasn't permissions to this advert's image");
        
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
        var advertId = (await _repository.GetById(id, cancellationToken))!.AdvertId;
        
        var existEntity = await _advertService.GetById(advertId, cancellationToken);
        var authorizationResult = await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext!.User, existEntity, OperationsList.Update);
        if (!authorizationResult.Succeeded) throw new UnauthorizedAccessException("User hasn't permissions to this advert's image");
        
        return await _repository.Delete(id, cancellationToken);
    }
}