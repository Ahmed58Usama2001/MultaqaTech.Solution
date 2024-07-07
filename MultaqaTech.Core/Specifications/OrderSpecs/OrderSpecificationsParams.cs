namespace MultaqaTech.Core.Specifications.OrderSpecs;

public class OrderSpecificationsParams
{
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }

    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    public string? UserEmail { get; set; }

    private const int _maxPageSize = 12;
    private int _pageSize = 6;

    public int PageSize
    {
        get { return _pageSize; }
        set { _pageSize = value > _maxPageSize ? _maxPageSize : value; }
    }

    public int PageIndex { get; set; } = 1;
}