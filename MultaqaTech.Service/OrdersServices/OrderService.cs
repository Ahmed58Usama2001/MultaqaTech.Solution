namespace MultaqaTech.Service.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseService _courseService;
        private readonly MultaqaTechContext _context;
        private readonly IBasketRepository _basketRepository;

        public OrderService(IUnitOfWork unitOfWork, ICourseService courseService, MultaqaTechContext context, IBasketRepository basketRepository)
        {
            _unitOfWork = unitOfWork;
            _courseService = courseService;
            _context = context;
            _basketRepository = basketRepository;
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            order.Basket = await _basketRepository.GetBasket(order.UserEmail);
            await BeforeCreate(order);
            await _unitOfWork.Repository<Order>().AddAsync(order);
            await _unitOfWork.CompleteAsync();
            await AfterCreate(order);

            return order;
        }

        private async Task BeforeCreate(Order order)
        {
            order.Status = OrderStatus.Completed; // Waiting Payment Part
            order.CreationDate = DateTime.Now;
            order.TotalPriceBeforeDiscount = order.Basket?.BasketItems?.Sum(item => item.Price) ?? 0;
            order.PriceAfterDiscount = order.Basket?.BasketItems?.Sum(item => item.Price) ?? 0;
            await Task.CompletedTask;
        }

        private async Task AfterCreate(Order order)
        {
            await ManageStudentCourses(order);
            await ManageCoursesCachedStudentsIds(order);
            await DeleteBasket(order);
        }

        private async Task DeleteBasket(Order order)
        {
            await _basketRepository.DeleteBasket(order.UserEmail);
        }

        private async Task ManageCoursesCachedStudentsIds(Order order)
        {
            if (order.Basket?.BasketItems == null) return;

            foreach (BasketItem item in order.Basket.BasketItems)
            {
                Course? courseToBeUpdated = await _courseService.ReadByIdAsync(item.CourseId);
                if (courseToBeUpdated != null)
                {
                    courseToBeUpdated.EnrolledStudentsIds.Add(order.Basket.StudentId);
                    await _courseService.UpdateCourse(courseToBeUpdated, courseToBeUpdated.Id);
                }
            }
        }

        private async Task ManageStudentCourses(Order order)
        {
            var studentCourses = PrepareStudentCourses(order);

            if (studentCourses?.Count > 0)
            {
                await CreateStudentCourses(studentCourses);

                var studentProgresses = new List<StudentCourseProgress>();
                foreach (var studentCourse in studentCourses)
                {
                    var progresses = await PrepareStudentsProgress(studentCourse);
                    if (progresses != null)
                    {
                        studentProgresses.AddRange(progresses);
                    }
                }

                if (studentProgresses.Count > 0)
                {
                    await CreateStudentProgressesInCourse(studentProgresses);
                }
            }
        }

        private List<StudentCourse>? PrepareStudentCourses(Order order)
        {
            if (order.Basket?.BasketItems == null) return null;

            return order.Basket.BasketItems.Select(basketItem => new StudentCourse
            {
                CourseId = basketItem.CourseId,
                StudentId = order.Basket.StudentId,
            }).ToList();
        }

        private async Task CreateStudentCourses(List<StudentCourse> studentCourses)
        {
            await _unitOfWork.Repository<StudentCourse>().BulkAddAsync(studentCourses);
            await _unitOfWork.CompleteAsync(); // Ensure the IDs are generated
        }

        private async Task<List<StudentCourseProgress>> PrepareStudentsProgress(StudentCourse studentCourse)
        {
            if (studentCourse is null || studentCourse.CourseId == 0 || studentCourse.StudentId == 0)
                return new List<StudentCourseProgress>();

            var course = await _context.Courses
                .Include(c => c.CurriculumSections)
                .ThenInclude(cs => cs.Lectures)
                .Include(c => c.CurriculumSections)
                .ThenInclude(cs => cs.Quizes)
                .FirstOrDefaultAsync(c => c.Id == studentCourse.CourseId);

            if (course == null) return new List<StudentCourseProgress>();

            var studentProgresses = new List<StudentCourseProgress>();

            foreach (var section in course.CurriculumSections)
            {
                foreach (var lecture in section.Lectures)
                {
                    if (await _context.Lectures.AnyAsync(l => l.Id == lecture.Id))
                    {
                        studentProgresses.Add(new StudentCourseProgress
                        {
                            StudentCourseId = studentCourse.Id,
                            LectureId = lecture.Id,
                            IsCompleted = false
                        });
                    }
                }

                foreach (var quiz in section.Quizes)
                {
                    if (await _context.Quizes.AnyAsync(q => q.Id == quiz.Id))
                    {
                        studentProgresses.Add(new StudentCourseProgress
                        {
                            StudentCourseId = studentCourse.Id,
                            QuizId = quiz.Id,
                            IsCompleted = false
                        });
                    }
                }
            }

            return studentProgresses;
        }


        private async Task CreateStudentProgressesInCourse(List<StudentCourseProgress> studentProgresses)
        {
            await _unitOfWork.Repository<StudentCourseProgress>().BulkAddAsync(studentProgresses);
            await _unitOfWork.CompleteAsync(); // Ensure the IDs are generated
        }

        public async Task<IEnumerable<Order>> ReadOrdersAsync(OrderSpecificationsParams orderSpecificationsParams)
        {
            OrderSpecifications orderSpecs = new(orderSpecificationsParams);
            return await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(orderSpecs);
        }
    }
}
