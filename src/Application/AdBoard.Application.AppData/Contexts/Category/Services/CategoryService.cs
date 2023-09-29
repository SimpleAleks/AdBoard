using AdBoard.Application.AppData.Contexts.Category.Repositories;
using AdBoard.Application.AppData.Contexts.Category.Services.Helpers;
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

    /// <inheritdoc />
    public Task<ShortCategoryDto[]> GetAll(CancellationToken cancellationToken)
    {
        return _repository.GetAll(cancellationToken);
    }

    /// <inheritdoc />
    public Task<CategoryDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return _repository.GetById(id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ShortCategoryDto> Add(CreateCategoryDto dto, CancellationToken cancellationToken)
    {
        return await _repository.Add(_mapper.Map<Category>(dto), cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ShortCategoryDto> Update(Guid id, UpdateCategoryDto dto, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(dto);
        return await _repository.Update(id, category, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        return await _repository.Delete(id, cancellationToken);
    }

    /// <inheritdoc />
    public Task ImportAsync(IEnumerable<ImportCategoryDto> importCategories, CancellationToken cancellationToken)
    {
        var entities = CategoriesHelper.MapImportCategoriesToDomain(importCategories);
        return _repository.AddRange(entities, cancellationToken);
    }
}