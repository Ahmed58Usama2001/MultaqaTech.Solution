using MultaqaTech.Core.Entities.OrderEntities;
using MultaqaTech.Core.Services.Contract.OrderContracts;
using Order = MultaqaTech.Core.Entities.OrderEntities.Order;
namespace MultaqaTech.OrderService;

public class OrderService(IUnitOfWork unitOfWork) : IOrderService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    private async Task BeforeCreate(Order order)
    {
        order.Status = OrderStatus.Completed; // Waiting Payment Part
        order.CreationDate = DateTime.Now;
    }
    public async Task<bool> CreateOrderAsync(Order order)
    {
        await BeforeCreate(order);
        await _unitOfWork.Repository<Order>().AddAsync(order);
        await AfterCreate(order);

        int result =  await _unitOfWork.CompleteAsync();

        return result > 0;
    }

    private async Task AfterCreate(Order order)
    {
        await ManageStudentCourses(order);
    }

    private async Task ManageStudentCourses(Order order)
    {
        List<StudentCourse>? studentCourses =  PrepareStudentCourses(order) ;
        await CreateStudentCourses(studentCourses);
    }

    private  List<StudentCourse> PrepareStudentCourses(Order order)
    {
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