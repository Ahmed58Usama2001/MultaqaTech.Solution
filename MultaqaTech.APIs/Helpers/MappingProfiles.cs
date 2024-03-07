using MultaqaTech.APIs.Dtos.BlogPostDtos;
using MultaqaTech.Core.Entities.BlogPostDomainEntities;

namespace MultaqaTech.APIs.Helpers
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {

            CreateMap<SubjectCreateDto, Subject>().ReverseMap();

            CreateMap<BlogPostCategoryCreateDto, BlogPostCategory>();

            CreateMap<BlogPost, BlogPostToReturnDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags != null ? src.Tags.Select(s => s.Name).ToList() : null))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments != null ? src.Comments.Select(c => c.CommentContent).ToList() : null))
                .ForMember(dest => dest.PublishingDate, opt => opt.MapFrom(src => src.PublishingDate.ToString("dddd, MMMM dd, yyyy 'at' hh:mm:ss tt")));

            CreateMap<BlogPostComment, BlogPostCommentToReturnDto>()
                .ForMember(dest => dest.BlogPost, opt => opt.MapFrom(src => src.BlogPost.Title))
                .ForMember(dest => dest.DatePosted, opt => opt.MapFrom(src => src.DatePosted.ToString("dddd, MMMM dd, yyyy 'at' hh:mm:ss tt")));
        }

    }
}
