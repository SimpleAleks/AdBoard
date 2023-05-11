using AdBoard.Contracts.Comment;
using AutoMapper;

namespace AdBoard.Infrastructure.MapProfiles;

using Comment = AdBoard.Domain.Comment.Comment;

public class CommentProfile : Profile
{
    public CommentProfile()
    {
        CreateMap<Comment, CommentDto>(MemberList.Destination)
            .ForMember(c => c.Id, map => map.MapFrom(c => c.Id))
            .ForMember(c => c.Text, map => map.MapFrom(c => c.Text))
            .ForMember(c => c.Created, map => map.MapFrom(c => c.Created))
            .ForMember(c => c.Advert, map => map.MapFrom(c => c.Advert))
            .ForMember(c => c.User, map => map.MapFrom(c => c.User))
            .ForMember(c => c.ParentId, map => map.MapFrom(c => c.ParentId));

        CreateMap<Comment, ShortCommentDto>(MemberList.Destination)
            .ForMember(c => c.Id, map => map.MapFrom(c => c.Id))
            .ForMember(c => c.Text, map => map.MapFrom(c => c.Text))
            .ForMember(c => c.Created, map => map.MapFrom(c => c.Created))
            .ForMember(c => c.User, map => map.MapFrom(c => c.User))
            .ForMember(c => c.ParentId, map => map.MapFrom(c => c.ParentId));

        CreateMap<CreateCommentDto, Comment>(MemberList.Source)
            .ForMember(c => c.Text, map => map.MapFrom(c => c.Text))
            .ForMember(c => c.ParentId, map => map.MapFrom(c => c.ParentId));

        CreateMap<UpdateCommentDto, Comment>(MemberList.Source)
            .ForMember(c => c.Text, map => map.MapFrom(c => c.Text));
    }
}