namespace MultaqaTech.APIs.Helpers
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {

            CreateMap<SubjectDto, Subject>().ReverseMap();

            CreateMap<BlogPostCategoryDto, BlogPostCategory>().ReverseMap();

            CreateMap<BlogPost, BlogPostDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags != null ? src.Tags.Select(s => s.Name).ToList() : null))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments != null ? src.Comments.Select(c => c.CommentContent).ToList() : null))
                .ForMember(dest => dest.PublishingDate, opt => opt.MapFrom(src => src.PublishingDate.ToString("dddd, MMMM dd, yyyy 'at' hh:mm:ss tt")));

            //CreateMap<BlogPostDto, BlogPost>()
            //    .ForMember(dest => dest.Category, opt => opt.MapFrom(src => new BlogPostCategory { Id=src.CategoryId, Name = src.Category }))
            //    .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => MapTags(src.Tags)))
            //    .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => MapComments(src.Comments)))
            //    .ForMember(dest => dest.PublishingDate, opt => opt.MapFrom(src => DateTime.ParseExact(src.PublishingDate, "dddd, MMMM dd, yyyy 'at' hh:mm:ss tt", CultureInfo.InvariantCulture)));

            CreateMap<CourseDto, Course>().ReverseMap();
            CreateMap<Course, CourseToReturnDto>().ReverseMap();
        }
    }
}