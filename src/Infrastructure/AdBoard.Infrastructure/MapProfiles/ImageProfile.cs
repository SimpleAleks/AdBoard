using AdBoard.Contracts.Image;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace AdBoard.Infrastructure.MapProfiles;

using Image = AdBoard.Domain.Image.Image;

public class ImageProfile : Profile
{
    public ImageProfile()
    {
        CreateMap<CreateImageDto, Image>(MemberList.None)
            .ForMember(d => d.AdvertId, map => map.MapFrom(s => s.AdvertId))
            .ForMember(d => d.Content, map => map.MapFrom((s, _, _, _) => FileToBytes(s.Content).Result));

        CreateMap<Image, ShortImageDto>(MemberList.None)
            .ForMember(d => d.Id, map => map.MapFrom(s => s.Id));

        CreateMap<Image, ImageDto>(MemberList.None)
            .ForMember(d => d.Id, map => map.MapFrom(s => s.Id))
            .ForMember(d => d.Content, map => map.MapFrom(s => s.Content))
            .ForMember(d => d.AdvertId, map => map.MapFrom(s => (Guid)s.AdvertId!));
    }
    
    /// <summary>
    /// Переводит изображение в массив байтов
    /// </summary>
    /// <param name="file">Изображение</param>
    /// <returns>Массивы <see cref="Image"/></returns>
    private async Task<byte[]> FileToBytes(IFormFile file)
    {
        await using var fileStream = file.OpenReadStream();
        var bytes = new byte[file.Length];
        await fileStream.ReadAsync(bytes.AsMemory(0, (int)file.Length));
        return bytes;
    }
}