namespace MultaqaTech.Core.Specifications.EventEntitiesSpecs.EventSpecs
{
    public class EventSpeceficationsParams
    {
        public string? sort { get; set; }
        public int? categoryId { get; set; }
        public int? countryId { get; set; }
        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }


        private const int maxPageSize = 12;
        private int pageSize = 6;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > maxPageSize ? maxPageSize : value; }
        }

        public int PageIndex { get; set; } = 1;
    }
}
