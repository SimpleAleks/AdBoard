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
        CreateMap<User, UserDto>()
            .ForMember(d => d.Id, map => map.MapFrom(s => s.Id))
            .ForMember(d => d.Name, map => map.MapFrom(s => s.Name))
            .ForMember(d => d.Adverts, map => map.MapFrom(s => s.Adverts))
            .ForMember(d => d.RegisteredTime, map => map.MapFrom(s => s.RegisteredTime));

        CreateMap<User, ShortUserDto>()
            .ForMember(d => d.Id, map => map.MapFrom(s => s.Id))
            .ForMember(d => d.Name, map => map.MapFrom(s => s.Name));

        CreateMap<CreateUserDto, User>(MemberList.None)
            .ForMember(d => d.Name, map => map.MapFrom(s => s.Name))
            .ForMember(d => d.Login, map => map.MapFrom(s => s.Login))
            .ForMember(d => d.Password, map => map.MapFrom(s => s.Password))
            .ForMember(d => d.RegisteredTime, map => map.MapFrom(s => DateTime.UtcNow));

        CreateMap<UpdateUserDto, User>(MemberList.None).IncludeBase<CreateUserDto, User>();
        
    }
}