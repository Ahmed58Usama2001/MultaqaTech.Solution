namespace MultaqaTech.Repository.Repositories;

public class BasketRepository(IConnectionMultiplexer redis) : IBasketRepository
{
    private readonly IDatabase _context = redis.GetDatabase();

    public async Task<StudentBasket?> UpdateBasket(StudentBasket studentBasket, string email)
    {
        bool updateOrCreatedSuccessfully = await _context.StringSetAsync(email, JsonSerializer.Serialize(studentBasket), TimeSpan.FromDays(1));
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

        await UpdateBasket(basketFromDb, email);

        return basketFromDb;
    }

    public async Task<StudentBasket?> AddCourseToBasket(string email, BasketItem basketItem)
    {
        StudentBasket? basketFromDb = await GetBasket(email);

        if (basketFromDb == null) return null;

        basketFromDb.BasketItems?.Add(basketItem);

        await UpdateBasket(basketFromDb, email);

        return basketFromDb;
    }

    public async Task<bool> DeleteBasket(string email)
        => await _context.KeyDeleteAsync(email);
}