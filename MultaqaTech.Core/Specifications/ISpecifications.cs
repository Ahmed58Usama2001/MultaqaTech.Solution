namespace MultaqaTech.Core.Specifications;

public interface ISpecifications<T> where T : BaseEntity
{
    public Expression<Func<T, bool>> Criteria { get; set; }

    public List<Expression<Func<T,object>>> Includes { get; set; }

    public Expression<Func<T,object>> OrderBy { get; set; }
    public Expression<Func<T,object>> OrderByDesc { get; set; }

    public int Take { get; set; }
    public int Skip { get; set; }

    public bool IsPaginationEnabled { get; set; }
}
