namespace MultaqaTech.Repository.Repositories;

public class BasketRepository(IConnectionMultiplexer redis, IUnitOfWork unitOfWork, ICourseService courseService) : IBasketRepository
{
    private readonly IDatabase _context = redis.GetDatabase();
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ICourseService _courseService = courseService;

    public async Task<StudentBasket?> UpdateBasket(string email, params int[] coursesIds)
    {
        List<BasketItem>? basketItems = await PrepareBasketItems(coursesIds);
        StudentBasket studentBasket = new() { BasketItems = basketItems };

        bool updateOrCreatedSuccessfully = await SaveBasketToDb(email, studentBasket);
        return updateOrCreatedSuccessfully ? studentBasket : null;
    }

    public async Task<StudentBasket?> GetBasket(string email)
    {
        RedisValue basketAsJson = await _context.StringGetAsync(email);

        return basketAsJson.IsNull ? null : JsonSerializer.Deserialize<StudentBasket>(basketAsJson);
    }

    public async Task<StudentBasket?> RemoveCourseFromBasket(string email, int courseId)
    {
        StudentBasket? basketFromDb = await GetBasket(email);

        if (basketFromDb == null || basketFromDb.BasketItems is null || basketFromDb.BasketItems.Count == 0) return null;
        BasketItem? basketItemToBeRemoved = basketFromDb.BasketItems.Find(e => e.CourseId == courseId);

        if (basketItemToBeRemoved is null) return null;

        basketFromDb.BasketItems.Remove(basketItemToBeRemoved);

        await SaveBasketToDb(email, basketFromDb);

        return basketFromDb;
    }

    public async Task<StudentBasket?> AddCourseToBasket(string email, int courseId)
    {
        StudentBasket? basketFromDb = await GetBasket(email);

        List<BasketItem>? basketItem = await PrepareBasketItems(courseId);

        if (basketFromDb == null) return null;

        basketFromDb.BasketItems?.AddRange(basketItem);

        await SaveBasketToDb(email, basketFromDb);

        return basketFromDb;
    }

    private async Task<bool> SaveBasketToDb(string email, StudentBasket studentBasket)
         => await _context.StringSetAsync(email, JsonSerializer.Serialize(studentBasket), TimeSpan.FromDays(1));

    public async Task<bool> DeleteBasket(string email)
        => await _context.KeyDeleteAsync(email);

    private async Task<List<BasketItem>> PrepareBasketItems(params int[] coursesIds)
    {
        List<BasketItem> basketItems = [];
        IGenericRepository<Course>? coursesRepo = _unitOfWork.Repository<Course>();

        foreach (var courseId in coursesIds)
        {
            Course? course = await coursesRepo.GetByIdAsync(courseId) ?? throw new Exception("There is no course associated With provided course!");

            basketItems.Add(new()
            {
                CourseId = courseId,
                Price = course.Price,
                CourseTitle = course.Title,
                CourseThumbnailUrl = course.ThumbnailUrl,
            });
        }

        return basketItems;
    }
}