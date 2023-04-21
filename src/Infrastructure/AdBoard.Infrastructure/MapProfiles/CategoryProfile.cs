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
        CreateMap<Category, CategoryDto>()
            .ForMember(d => d.Id, map => map.MapFrom(s => s.Id))
            .ForMember(d => d.Name, map => map.MapFrom(s => s.Name))
            .ForMember(d => d.ParentId, map => map.MapFrom(s => s.ParentId))
            .ForMember(d => d.Childs, map => map.MapFrom(s => s.Childs));

        CreateMap<Category, ShortCategoryDto>()
            .ForMember(d => d.Id, map => map.MapFrom(s => s.Id))
            .ForMember(d => d.Name, map => map.MapFrom(s => s.Name))
            .ForMember(d => d.ParentId, map => map.MapFrom(s => s.ParentId));

        CreateMap<CreateCategoryDto, Category>(MemberList.None)
            .ForMember(d => d.Name, map => map.MapFrom(s => s.Name))
            .ForMember(d => d.ParentId, map => map.MapFrom(s => s.ParentId));

        CreateMap<UpdateCategoryDto, Category>(MemberList.None).IncludeBase<CreateCategoryDto, Category>();
    }
}