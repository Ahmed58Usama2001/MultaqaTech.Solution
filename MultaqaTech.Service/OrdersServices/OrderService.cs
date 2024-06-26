﻿namespace MultaqaTech.Service.OrderService;

public class OrderService(IUnitOfWork unitOfWork, ICourseService courseService, MultaqaTechContext context, IBasketRepository basketRepository) : IOrderService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICourseService _courseService = courseService;
    private readonly MultaqaTechContext _context = context;
    private readonly IBasketRepository _basketRepository = basketRepository;

    private async Task BeforeCreate(Order order)
    {
        order.Status = OrderStatus.Completed; // Waiting Payment Part
        order.CreationDate = DateTime.Now;
        order.TotalPriceBeforeDiscount = order.Basket?.BasketItems?.Sum(item => item.Price) ?? 0;
        order.PriceAfterDiscount = order.Basket?.BasketItems?.Sum(item => item.Price) ?? 0;

        await Task.CompletedTask;
    }

    public async Task<Order> CreateOrderAsync(Order order)
    {
        order.Basket = await _basketRepository.GetBasket(order.UserEmail);
        await BeforeCreate(order);
        await _unitOfWork.Repository<Order>().AddAsync(order);
        await AfterCreate(order);

        await _unitOfWork.CompleteAsync();

        return order;
    }

    private async Task AfterCreate(Order order)
    {
        await ManageStudentCourses(order);

        await ManageCoursesCachedStudentsIds(order);

        await DeleteBasket(order);
    }

    private async Task DeleteBasket(Order order)
         => await _basketRepository.DeleteBasket(order.UserEmail);

    private async Task ManageCoursesCachedStudentsIds(Order order)
    {
        if (order.Basket is null || order.Basket.BasketItems is null) return;

        foreach (BasketItem item in order.Basket.BasketItems)
        {
            Course? courseToBeUpdated = await _courseService.ReadByIdAsync(item.CourseId);
            if (courseToBeUpdated is not null)
            {
                courseToBeUpdated.EnrolledStudentsIds.Add(order.Basket.StudentId);
                await _courseService.UpdateCourse(courseToBeUpdated, courseToBeUpdated.Id);
            }
        }
    }

    private async Task ManageStudentCourses(Order order)
    {
        List<StudentCourse>? studentCourses = PrepareStudentCourses(order);

        if (studentCourses?.Count > 0)
        {
            await CreateStudentCourses(studentCourses);

            List<StudentCourseProgress>? studentProgresses = new List<StudentCourseProgress>();
            foreach (var studentCourse in studentCourses)
            {
                studentProgresses.AddRange(await PrepareStudentsProgress(studentCourse));
            }

            if (studentProgresses?.Count > 0)
            {
                await CreateStudentProgressesInCourse(studentProgresses);
            }
        }
    }

    private List<StudentCourse>? PrepareStudentCourses(Order order)
    {
        if (order.Basket is null || order.Basket.BasketItems is null) return null;

        List<StudentCourse> studentCourses = [];
        foreach (BasketItem basketItem in order.Basket.BasketItems)
        {
            studentCourses.Add(new()
            {
                CourseId = basketItem.CourseId,
                StudentId = order.Basket.StudentId,
            });
        }

        return studentCourses;
    }

    private async Task CreateStudentCourses(List<StudentCourse> studentCourses)
         => await _unitOfWork.Repository<StudentCourse>().BulkAddAsync(studentCourses);

    private async Task<List<StudentCourseProgress>?> PrepareStudentsProgress(StudentCourse studentCourse)
    {
        if (studentCourse is null || studentCourse.CourseId == 0 || studentCourse.StudentId == 0)
            return null;

        var course = await _context.Courses
       .Include(c => c.CurriculumSections)
         .ThenInclude(cs => cs.Lectures)
       .Include(c => c.CurriculumSections)
         .ThenInclude(cs => cs.Quizes)
       .Where(c => c.Id == studentCourse.CourseId)
       .FirstOrDefaultAsync();

        if (course == null)
            return null;

        var curriculumItems = new List<CurriculumItem>();
        foreach (var section in course.CurriculumSections)
        {
            curriculumItems.AddRange(section.Lectures);
            curriculumItems.AddRange(section.Quizes);
        }

        var studentProgresses = new List<StudentCourseProgress>();
        foreach (var curriculumItem in curriculumItems)
        {
            studentProgresses.Add(new StudentCourseProgress
            {
                StudentCourseId = studentCourse.Id,
                LectureId = curriculumItem.CurriculumItemType.Equals(CurriculumItemType.Lecture) ? curriculumItem.Id : 0,
                QuizId = curriculumItem.CurriculumItemType.Equals(CurriculumItemType.Quiz) ? curriculumItem.Id : 0,
                IsCompleted = false,
            });
        }

        return studentProgresses;
    }

    private async Task CreateStudentProgressesInCourse(List<StudentCourseProgress> studentProgresses)
     => await _unitOfWork.Repository<StudentCourseProgress>().BulkAddAsync(studentProgresses);
}