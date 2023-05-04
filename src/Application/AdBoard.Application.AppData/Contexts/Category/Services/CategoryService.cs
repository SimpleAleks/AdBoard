using AdBoard.Application.AppData.Contexts.Category.Repositories;
using AdBoard.Contracts.Category;
using AutoMapper;

namespace AdBoard.Application.AppData.Contexts.Category.Services;

using Category = AdBoard.Domain.Category.Category;

/// <inheritdoc cref="ICategoryService"/>
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует сервис по работе с категориями
    /// </summary>
    /// <param name="repository">Репозиторий категорий</param>
    /// <param name="mapper">Маппер</param>
    public CategoryService(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public Task<ShortCategoryDto[]> GetAll(CancellationToken cancellationToken)
    {
        return _repository.GetAll(cancellationToken);
    }

    public Task<CategoryDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return _repository.GetById(id, cancellationToken);
    }

    public async Task<ShortCategoryDto> Add(CreateCategoryDto dto, CancellationToken cancellationToken)
    {
        return await _repository.Add(_mapper.Map<Category>(dto), cancellationToken);
    }

    public async Task<ShortCategoryDto> Update(Guid id, UpdateCategoryDto dto, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(dto);
        return await _repository.Update(id, category, cancellationToken);
    }

    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.Delete(id, cancellationToken);
    }
}