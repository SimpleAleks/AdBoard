using AdBoard.Contracts.Advert;
using AdBoard.Domain.Advert;
using AdBoard.Domain.Image;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace AdBoard.Infrastructure.MapProfiles;

/// <summary>
/// Профиль маппера для <see cref="Advert"/>
/// </summary>
public class AdvertProfile : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public AdvertProfile()
    {
        CreateMap<CreateAdvertDto, Advert>(MemberList.None)
            .ForMember(x => x.Name, map => map.MapFrom(d => d.Name))
            .ForMember(x => x.Description, map => map.MapFrom(d => d.Description))
            .ForMember(x => x.IsActive, map => map.MapFrom(d => true))
            .ForMember(x => x.Images, map => map.MapFrom((s, _, _, _) => RequestFilesToImages(s.Images)))
            .ForMember(x => x.Cost, map => map.MapFrom(d => d.Cost))
            .ForMember(x => x.Email, map => map.MapFrom(d => d.Email))
            .ForMember(x => x.Phone, map => map.MapFrom(d => d.Phone))
            .ForMember(x => x.Location, map => map.MapFrom(d => d.Location))
            .ForMember(x => x.Created, map => map.MapFrom(d => DateTime.UtcNow))
            .ForMember(x => x.CategoryId, map => map.MapFrom(d => d.CategoryId));

        CreateMap<UpdateAdvertDto, Advert>(MemberList.None).IncludeBase<CreateAdvertDto, Advert>();

        CreateMap<Advert, AdvertDto>()
            .ForMember(d => d.Id, map => map.MapFrom(s => s.Id))
            .ForMember(d => d.Name, map => map.MapFrom(s => s.Name))
            .ForMember(d => d.Description, map => map.MapFrom(s => s.Description))
            .ForMember(x => x.IsActive, map => map.MapFrom(d => d.IsActive))
            .ForMember(d => d.ImagesIds, map => map.MapFrom(s => s.Images.Select(i => (Guid)i.Id!)))
            .ForMember(d => d.Cost, map => map.MapFrom(d => d.Cost))
            .ForMember(d => d.Email, map => map.MapFrom(d => d.Email))
            .ForMember(d => d.Phone, map => map.MapFrom(d => d.Phone))
            .ForMember(d => d.Location, map => map.MapFrom(d => d.Location))
            .ForMember(d => d.Created, map => map.MapFrom(d => DateTime.UtcNow))
            .ForMember(d => d.User, map => map.MapFrom(s => s.User))
            .ForMember(d => d.Category, map => map.MapFrom(s => s.Category));
            

        CreateMap<Advert, ShortAdvertDto>()
            .ForMember(d => d.Id, map => map.MapFrom(s => s.Id))
            .ForMember(d => d.Name, map => map.MapFrom(s => s.Name))
            .ForMember(d => d.Location, map => map.MapFrom(s => s.Location))
            .ForMember(d => d.CreatedAt, map => map.MapFrom(s => s.Created))
            .ForMember(x => x.IsActive, map => map.MapFrom(d => d.IsActive))
            .ForMember(d => d.ImagesIds, map => map.MapFrom(s => s.Images.Select(i => (Guid)i.Id!)))
            .ForMember(d => d.Cost, map => map.MapFrom(d => d.Cost));
    }

    /// <summary>
    /// Переводит полученные из запроса файлы (<see cref="IFormFile"/>) в массив изображений <see cref="Image"/>
    /// </summary>
    /// <param name="files">Файлы полученные из запроса</param>
    /// <returns>Массивы <see cref="Image"/></returns>
    private IEnumerable<Image> RequestFilesToImages(IEnumerable<IFormFile> files)
    {
        foreach (var file in files)
        {
            using var fileStream = file.OpenReadStream();
            byte[] bytes = new byte[file.Length];
            fileStream.Read(bytes, 0, (int)file.Length);
            yield return new Image()
            {
                Id = null,
                Advert = null,
                AdvertId = null,
                Content = bytes
            };
        }
    }
}