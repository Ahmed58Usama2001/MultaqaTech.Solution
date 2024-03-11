namespace MultaqaTech.Service;

public class CourseReviewService(IUnitOfWork unitOfWork) : ICourseReviewService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
   
}