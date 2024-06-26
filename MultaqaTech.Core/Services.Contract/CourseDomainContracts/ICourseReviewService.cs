﻿namespace MultaqaTech.Core.Services.Contract;

public interface ICourseReviewService
{
    Task<CourseReview?> CreateCourseReviewAsync(CourseReview review, Entities.Identity.AppUser? student);
    Task<bool> DeleteCourseReviewAsync(CourseReview review);
    Task<CourseReview?> UpdateCourseReviewAsync(CourseReview review);
}