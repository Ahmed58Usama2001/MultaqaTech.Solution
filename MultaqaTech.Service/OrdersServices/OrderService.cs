namespace MultaqaTech.OrderService;

public class OrderService(IUnitOfWork unitOfWork, ICourseService courseService) : IOrderService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICourseService _courseService = courseService;

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
    }

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
            await CreateStudentCourses(studentCourses);
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
}