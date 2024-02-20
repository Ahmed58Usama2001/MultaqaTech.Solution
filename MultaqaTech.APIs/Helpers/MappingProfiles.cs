using MultaqaTech.Core.Entities;

namespace MultaqaTech.APIs.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<SubjectDto, Subject>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<Subject, SubjectDto>();
    }
}