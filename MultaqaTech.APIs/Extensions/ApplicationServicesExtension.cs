﻿using MultaqaTech.Core.Services.Contract.ZoomMeetingContracts;
using MultaqaTech.Service.ZoomMeetingEntitesServices;

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
        services.AddScoped<IBlogPostCategoryService, BlogPostCategoryService>();
        services.AddScoped<IBlogPostService, BlogPostService>();
        services.AddScoped<IBlogPostCommentService, BlogPostCommentService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<ICourseReviewService, CourseService>();
        services.AddScoped<IZoomMeetingCategoryService , ZoomMeetingCategoryService>();

        return services;
    }
}
