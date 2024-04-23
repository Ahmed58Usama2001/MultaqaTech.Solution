﻿namespace MultaqaTech.Service.CourseDomainServices;

public partial class CourseService
{
    public async Task<CourseReview?> CreateCourseReviewAsync(CourseReview review, Core.Entities.Identity.AppUser? student)
    {
        try
        {
            review.StudentName = student?.FirstName + " " + student?.LastName;

            Course? courseFromDb = await _unitOfWork.Repository<Course>().GetByIdAsync(review.CourseId);
            review.Id = courseFromDb?.Reviews is null ? 1 : courseFromDb.Reviews.Count;

            courseFromDb?.Reviews?.Add(review);

            if (courseFromDb is not null)
                _unitOfWork.Repository<Course>().Update(courseFromDb);

            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return null;

            return review;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<CourseReview?> UpdateCourseReviewAsync(CourseReview review)
    {
        try
        {
            Course? courseFromDb = await _unitOfWork.Repository<Course>().GetByIdAsync(review.CourseId);
            if (courseFromDb == null || courseFromDb.Reviews == null)
                return null;

            CourseReview? reviewToBeUpdated = courseFromDb.Reviews.Find(cr => cr.Id.Equals(review.Id));
            if (reviewToBeUpdated == null)
                return null;

            reviewToBeUpdated.Content = review.Content;
            reviewToBeUpdated.NumberOfStars = review.NumberOfStars;

            _unitOfWork.Repository<Course>().Update(courseFromDb);
            int result = await _unitOfWork.CompleteAsync();

            if (result <= 0)
                return null;
            return review;
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return null;
        }
    }

    public async Task<bool> DeleteCourseReviewAsync(CourseReview review)
    {
        try
        {
            Course? courseFromDb = await _unitOfWork.Repository<Course>().GetByIdAsync(review.CourseId);

            CourseReview reviewToBeRemoved = courseFromDb?.Reviews?.Find(cr => cr.Id.Equals(review.Id)) ?? new();
            courseFromDb?.Reviews?.Remove(reviewToBeRemoved);

            if (courseFromDb is not null)
                _unitOfWork.Repository<Course>().Update(courseFromDb);

            var result = await _unitOfWork.CompleteAsync();
            if (result <= 0) return false;

            return true;
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return false;
        }
    }
}