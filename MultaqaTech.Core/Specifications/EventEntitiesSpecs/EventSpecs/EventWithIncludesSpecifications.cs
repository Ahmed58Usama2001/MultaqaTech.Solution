namespace MultaqaTech.Core.Specifications.EventEntitiesSpecs.EventSpecs
{
    public class EventWithIncludesSpecifications : BaseSpecifications<Event>
    {
        public EventWithIncludesSpecifications(EventSpeceficationsParams speceficationsParams) :
            base(p =>
              (!speceficationsParams.categoryId.HasValue || p.EventCategoryId == speceficationsParams.categoryId.Value)
            &&
            (!speceficationsParams.countryId.HasValue || p.EventCountryId == speceficationsParams.countryId.Value)
            &&
            (string.IsNullOrEmpty(speceficationsParams.Search)
              || p.Title.ToLower().Contains(speceficationsParams.Search)
            ))

        {

            AddIncludes();

            if (!string.IsNullOrEmpty(speceficationsParams.sort))
            {
                switch (speceficationsParams.sort)
                {

                    case "StartingDateAsc":
                        AddOrderBy(p => p.DateFrom);
                        break;
                    case "StartingDateDesc":
                        AddOrderBy(p => p.DateFrom);
                        break;

                    default:
                        AddOrderByDesc(p => p.DateFrom);
                        break;
                }
            }
            else
                AddOrderByDesc(p => p.DateFrom);

            ApplyPagination((speceficationsParams.PageIndex - 1) * speceficationsParams.PageSize, speceficationsParams.PageSize);

        }

        public EventWithIncludesSpecifications(int id)
            : base(p => p.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(p => p.Category);
            Includes.Add(p => p.Country);
            Includes.Add(p => p.Comments);
            Includes.Add(p => p.Speakers);

        }

    }
}
