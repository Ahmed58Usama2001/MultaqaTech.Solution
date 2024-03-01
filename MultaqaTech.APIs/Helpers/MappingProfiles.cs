namespace MultaqaTech.APIs.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<SubjectDto, Subject>().ReverseMap();
        CreateMap<BlogPostCategoryDto, BlogPostCategory>().ReverseMap();
        CreateMap<CourseDto, Course>().ReverseMap();
        CreateMap<Course, CourseToReturnDto>().ReverseMap();
    }
}