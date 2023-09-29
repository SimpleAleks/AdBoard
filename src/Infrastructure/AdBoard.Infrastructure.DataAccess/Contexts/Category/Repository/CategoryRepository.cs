using AdBoard.Application.AppData.Contexts.Category.Repositories;
using AdBoard.Contracts.Category;
using AdBoard.Infrastructure.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AdBoard.Infrastructure.DataAccess.Contexts.Category.Repository;

using Category = AdBoard.Domain.Category.Category;

/// <inheritdoc cref="ICategoryRepository"/>
public class CategoryRepository : ICategoryRepository
{
    private readonly IRepository<Category> _repository;
    private readonly IMapper _mapper;
    
    public CategoryRepository(IRepository<Category> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public Task<ShortCategoryDto[]> GetAll(CancellationToken cancellationToken)
    {
        return _repository.GetAll()
            .ProjectTo<ShortCategoryDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }

    public Task<CategoryDto?> GetById(Guid id, CancellationToken cancellationToken)
    {
        return _repository.GetAll()
            .Where(c => c.Id == id)
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ShortCategoryDto> Add(Category category, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(category, cancellationToken);
        return _mapper.Map<ShortCategoryDto>(category);
    }

    public Task AddRange(IEnumerable<Category> entities, CancellationToken cancellationToken)
    {
        return _repository.AddRangeAsync(entities, cancellationToken);
    }

    public async Task<ShortCategoryDto> Update(Guid id, Category category, CancellationToken cancellationToken)
    {
        category.Id = id;
        await _repository.UpdateAsync(category, cancellationToken);
        return _mapper.Map<ShortCategoryDto>(category);
    }

    public async Task<bool> Delete(Guid id, CancellationToken cancellationToken)
    {
        var model = await _repository.GetByIdAsync(id, cancellationToken);
        if (model is null) return false;
        await _repository.DeleteAsync(model, cancellationToken);
        return true;
    }
}