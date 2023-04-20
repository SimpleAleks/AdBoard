using AdBoard.Contracts.Advert;
using AdBoard.Contracts.User;
using AdBoard.Domain.User;
using AutoMapper;

namespace AdBoard.Infrastructure.MapProfiles;

/// <summary>
/// Профиль маппера для <see cref="User"/>
/// </summary>
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, ShortUserDto>()
            .ForMember(d => d.Id, map => map.MapFrom(s => s.Id))
            .ForMember(d => d.Name, map => map.MapFrom(s => s.Name));
    }
}