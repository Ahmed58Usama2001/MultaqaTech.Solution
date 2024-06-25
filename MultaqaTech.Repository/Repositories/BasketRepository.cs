using Microsoft.AspNetCore.Identity;

namespace MultaqaTech.Repository.Repositories;

public class BasketRepository(IConnectionMultiplexer redis, IUnitOfWork unitOfWork, MultaqaTechContext multaqaTechContext, UserManager<AppUser> userManager) : IBasketRepository
{
    private readonly IDatabase _context = redis.GetDatabase();
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly MultaqaTechContext _multaqaTechContext = multaqaTechContext;
    private readonly UserManager<AppUser> _userManager = userManager;


    public async Task<StudentBasket?> UpdateBasket(string email, params int[] coursesIds)
    {
        List<BasketItem>? basketItems = await PrepareBasketItems(coursesIds);
        StudentBasket studentBasket = new() 
        {
            BasketItems = basketItems,
            StudentId = _userManager.FindByEmailAsync(email).Id,
        };

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
        BasketItem? basketItemToBeRemoved = basketFromDb.BasketItems.Find(e => e.CourseId == courseId)
             ?? throw new Exception("Baket Containin No Course with Provided Id!!");

        basketFromDb.BasketItems.Remove(basketItemToBeRemoved);

        await SaveBasketToDb(email, basketFromDb);

        return basketFromDb;
    }

    public async Task<StudentBasket?> AddCourseToBasket(string email, int courseId)
    {
        StudentBasket? basketFromDb = await GetBasket(email);

        if (basketFromDb is null || basketFromDb.BasketItems is null)
        {
            basketFromDb = new()
            {
                BasketItems = [],
                StudentId = (await _multaqaTechContext.Set<AppUser>().FirstOrDefaultAsync(e => e.Email == email)).StudentId ?? 0
            };
        }

        var basketItemFromDb = basketFromDb.BasketItems.Find(e => e.CourseId == courseId);

        if (basketItemFromDb is not null) throw new Exception("Course with same id already exists in your basket");

        var basketItem = await PrepareBasketItems(courseId);

        basketFromDb.BasketItems.AddRange(basketItem);

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
            Course? course = await coursesRepo.GetByIdAsync(courseId) ?? throw new Exception("There is no course associated With provided courseId!");

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