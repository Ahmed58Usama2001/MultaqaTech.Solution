namespace MultaqaTech.Core.Repositories.Contract;

public interface IBasketRepository
{
    public Task<StudentBasket?> UpdateBasket(string email, params int[] coursesIds);
    public Task<StudentBasket?> GetBasket(string email);
    public Task<bool> DeleteBasket(string email);
    public Task<StudentBasket?> AddCourseToBasket(string email, int courseId);
    public Task<StudentBasket?> RemoveCourseFromBasket(string email, int courseId);
}