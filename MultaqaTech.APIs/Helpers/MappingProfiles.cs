namespace MultaqaTech.APIs.Helpers;

public class MappingProfiles : Profile
{

    public MappingProfiles()
    {

        CreateMap<SubjectCreateDto, Subject>().ReverseMap();

        #region Blog Post

        CreateMap<BlogPostCategoryCreateDto, BlogPostCategory>();
        CreateMap<BlogPostCreateDto, BlogPost>()
            .ForMember(dest => dest.PostPictureUrl, opt => opt.Ignore());

        CreateMap<BlogPost, BlogPostToReturnDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.PublishingDate, opt => opt.MapFrom(src => src.PublishingDate.ToString("dddd, MMMM dd, yyyy 'at' hh:mm:ss tt")))
            .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<GenericMediaUrlResolver<BlogPost, BlogPostToReturnDto>>());

        CreateMap<BlogPostComment, BlogPostCommentToReturnDto>()
            .ForMember(dest => dest.BlogPost, opt => opt.MapFrom(src => src.BlogPost.Title))
            .ForMember(dest => dest.DatePosted, opt => opt.MapFrom(src => src.DatePosted.ToString("dddd, MMMM dd, yyyy 'at' hh:mm:ss tt")));
        #endregion


        #region Courses

        CreateMap<CourseDto, Course>()
           .ForMember(dest => dest.PrerequisitesIds, opt => opt.MapFrom(src => src.PrerequisitesIds))
           .ForMember(dest => dest.TagsIds, opt => opt.MapFrom(src => src.TagsIds));

        CreateMap<Course, CourseToReturnDto>()
           .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject.Name))
           //.ForMember(dest => dest.Instructor, opt => opt.MapFrom(src => src.Instructor.FirstName + src.Instructor.LastName))
           .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags != null ? src.Tags.Select(s => s.Name).ToList() : null))
           .ForMember(dest => dest.Prerequisites, opt => opt.MapFrom(src => src.Prerequisites != null ? src.Prerequisites.Select(c => c.Name).ToList() : null))
           .ForMember(d => d.ThumbnailUrl, O => O.MapFrom<GenericMediaUrlResolver<Course, CourseToReturnDto>>());


        CreateMap<CourseReviewDto, CourseReview>();
        CreateMap<CourseReview, CourseReviewToReturnDto>()
           .ForMember(d => d.ProfilePictureUrl, O => O.MapFrom<GenericMediaUrlResolver<CourseReview, CourseReviewToReturnDto>>());

        CreateMap<CurriculumSectionCreateDto, CurriculumSection>();
        CreateMap<CurriculumSectionUpdateDto, CurriculumSection>();
        CreateMap<CurriculumSection, CurriculumSectionReturnDto>();

        #endregion

        #region Zoom meetings
        CreateMap<ZoomMeetingCategoryCreateDto, ZoomMeetingCategory>();

        CreateMap<ZoomMeetingCreateDto, ZoomMeeting>()
            .ForMember(dest => dest.ZoomPictureUrl, opt => opt.Ignore());

        CreateMap<ZoomMeeting , ZoomMeetingToReturnDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate.ToString("dddd, MMMM dd, yyyy 'at' hh:mm:ss tt")))
            .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<GenericMediaUrlResolver<ZoomMeeting, ZoomMeetingToReturnDto>>());

        #endregion
    }
}