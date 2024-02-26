namespace MultaqaTech.APIs.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<TagDto, Tag>().ReverseMap(); 
        CreateMap<SubjectDto, Subject>().ReverseMap();
    }
}