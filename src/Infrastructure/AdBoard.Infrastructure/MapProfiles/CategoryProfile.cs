using AdBoard.Contracts.Category;
using AdBoard.Domain.Category;
using AutoMapper;

namespace AdBoard.Infrastructure.MapProfiles;

/// <summary>
/// Профиль маппера для <see cref="Category"/>
/// </summary>
public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, ShortCategoryDto>()
            .ForMember(d => d.Id, map => map.MapFrom(s => s.Id))
            .ForMember(d => d.Name, map => map.MapFrom(s => s.Name))
            .ForMember(d => d.ParentId, map => map.MapFrom(s => s.ParentId));
    }
}