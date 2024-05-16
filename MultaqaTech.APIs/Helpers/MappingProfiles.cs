using MultaqaTech.APIs.Dtos.CourseDtos.CurriculumDtos.QuizQuestionDtos;

namespace MultaqaTech.APIs.Helpers;

public class MappingProfiles : Profile
{

    public MappingProfiles()
    {

        CreateMap<SubjectCreateDto, Subject>().ReverseMap();

        CreateMap<Instructor, InstructorReturnDto>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.AppUser.InstructorId))
            .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.AppUser.FirstName + " " + src.AppUser.LastName));

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
           .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags != null ? src.Tags.Select(s => s.Name).ToList() : null))
           .ForMember(dest => dest.Prerequisites, opt => opt.MapFrom(src => src.Prerequisites != null ? src.Prerequisites.Select(c => c.Name).ToList() : null))
           .ForMember(d => d.ThumbnailUrl, O => O.MapFrom<GenericMediaUrlResolver<Course, CourseToReturnDto>>())
           .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Instructor.AppUser.FirstName + " " + src.Instructor.AppUser.LastName))
           .ForMember(dest => dest.InstructorPicture, opt => opt.MapFrom(src => src.Instructor.AppUser.ProfilePictureUrl));


        CreateMap<CourseReviewDto, CourseReview>();
        CreateMap<CourseReview, CourseReviewToReturnDto>()
           .ForMember(d => d.ProfilePictureUrl, O => O.MapFrom<GenericMediaUrlResolver<CourseReview, CourseReviewToReturnDto>>());

        CreateMap<CurriculumSectionCreateDto, CurriculumSection>();
        CreateMap<CurriculumSectionUpdateDto, CurriculumSection>();
        CreateMap<CurriculumSection, CurriculumSectionReturnDto>();

        CreateMap<QuizCreateDto, Quiz>()
        .ForMember(dest => dest.QuizQuestionPictureUrl, opt => opt.Ignore());
        CreateMap<QuizUpdateDto, Quiz>()
         .ForMember(dest => dest.QuizQuestionPictureUrl, opt => opt.Ignore());
        CreateMap<Quiz, QuizReturnDto>()
        .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<GenericMediaUrlResolver<Quiz, QuizReturnDto>>());

        CreateMap<LectureCreateDto, Lecture>()
        .ForMember(dest => dest.VideoUrl, opt => opt.Ignore());
        CreateMap<LectureUpdateDto, Lecture>()
        .ForMember(dest => dest.VideoUrl, opt => opt.Ignore());
        CreateMap<Lecture, LectureReturnDto>()
        .ForMember(dest => dest.VideoUrl, opt => opt.MapFrom<GenericMediaUrlResolver<Lecture, LectureReturnDto>>());

        CreateMap<NoteCreateDto, Note>();
        CreateMap<NoteUpdateDto, Note>();
        CreateMap<Note, NoteReturnDto>()
         .ForMember(dest => dest.PublishingDate, opt => opt.MapFrom(src => src.PublishingDate.ToString("dddd, MMMM dd, yyyy 'at' hh:mm:ss tt")));

        CreateMap<CurriculumItem, ItemReturnDto>()
          .ForMember(dest => dest.ItemType, opt => opt.MapFrom(src => src.CurriculumItemType.ToString()));

        CreateMap<QuizQuestionCreateDto, QuizQuestion>();
        CreateMap<QuizQuestionUpdateDto, QuizQuestion>();
        CreateMap<QuizQuestion, QuizQuestionReturnDto>();

        CreateMap<QuizQuestionChoiceCreateDto, QuizQuestionChoice>();
        CreateMap<QuizQuestionChoice, QuizQuestionChoiceReturnDto>();
        #endregion


        #region Event
        CreateMap<EventCategoryCreateDto, EventCategory>();

        CreateMap<EventSpeakerCreateDto, EventSpeaker>()
            .ForMember(dest => dest.SpeakerPictureUrl, opt => opt.Ignore());

        CreateMap<EventSpeaker , EventSpeakerToReturnDto>()
             .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom<GenericMediaUrlResolver<EventSpeaker, EventSpeakerToReturnDto>>());

        CreateMap<EventCreateDto , Event>()
            .ForMember(dest => dest.EventPictureUrl, opt => opt.Ignore());

        CreateMap<Event, EventToReturnDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.DateFrom, opt => opt.MapFrom(src => src.DateFrom.ToString("dddd, MMMM dd, yyyy")))
            .ForMember(dest => dest.DateTo, opt => opt.MapFrom(src => src.DateTo.ToString("dddd, MMMM dd, yyyy")));
           // .ForMember(dest => dest.TimeFrom, opt => opt.MapFrom(src => src.DateTo.ToString("h:mm tt")))
          //  .ForMember(dest => dest.TimeTo, opt => opt.MapFrom(src => src.DateTo.ToString("h:mm tt")));


        CreateMap<EventComment, EventCommentToReturnDto>()
            .ForMember(dest => dest.Event, opt => opt.MapFrom(src => src.Event.Title))
            .ForMember(dest => dest.DatePosted, opt => opt.MapFrom(src => src.DatePosted.ToString("dddd, MMMM dd, yyyy 'at' hh:mm:ss tt")));

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