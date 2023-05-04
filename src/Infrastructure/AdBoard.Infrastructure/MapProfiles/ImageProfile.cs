using AdBoard.Contracts.Image;
using AutoMapper;

namespace AdBoard.Infrastructure.MapProfiles;

using Image = AdBoard.Domain.Image.Image;

public class ImageProfile : Profile
{
    public ImageProfile()
    {
        CreateMap<Image, ShortImageDto>()
            .ForMember(d => d.Id, map => map.MapFrom(s => s.Id))
            .ForMember(d => d.Content, map => map.MapFrom(s => s.Content));
    }
}