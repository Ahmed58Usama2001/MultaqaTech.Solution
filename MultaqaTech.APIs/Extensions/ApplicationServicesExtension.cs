namespace MultaqaTech.APIs.Extensions;

public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

        services.AddAutoMapper(typeof(MappingProfiles));
        services.AddHttpClient();

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = (actionContext) =>
            {
                var errors = actionContext.ModelState.Where(p => p.Value?.Errors.Count() > 0)
                .SelectMany(p => p.Value?.Errors ?? new())
                .Select(e => e.ErrorMessage)
                .ToArray();

                var validationErrorResponse = new ApiValidationErrorResponse()
                {
                    Errors = errors
                };

                return new BadRequestObjectResult(validationErrorResponse);
            };
        });

        services.AddScoped<ISubjectService, SubjectService>();
        services.AddScoped<IMaillingService, MailingService>();

        services.AddScoped<IBlogPostCategoryService, BlogPostCategoryService>();
        services.AddScoped<IBlogPostService, BlogPostService>();
        services.AddScoped<IBlogPostCommentService, BlogPostCommentService>();

        services.AddScoped<IEventCategoryService, EventCategoryService>();
        services.AddScoped<IEventCommentService, EventCommentService>();
        services.AddScoped<IEventSpeakerService, EventSpeakerService>();
        services.AddScoped<IEventService, EventService>();

        services.AddScoped<IZoomMeetingCategoryService , ZoomMeetingCategoryService>();
        services.AddScoped<IZoomMeetingService , ZoomMeetingService>();

        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<ICourseReviewService, CourseService>();

        services.AddScoped<INoteService , NoteService>();
        services.AddScoped<IAnswerService , AnswerService>();
        services.AddScoped<IQuestionService , QuestionService>();
        services.AddScoped<IQuizQuestionService , QuizQuestionService>();
        services.AddScoped<IQuizQuestionChoiceService , QuizQuestionChoiceService>();
        services.AddScoped<ICurriculumItemService , CurriculumItemService>();
        services.AddScoped<ICurriculumSectionService , CurriculumSectionService>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}