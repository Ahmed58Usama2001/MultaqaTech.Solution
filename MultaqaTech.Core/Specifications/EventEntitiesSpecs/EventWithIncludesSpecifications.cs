
namespace MultaqaTech.Core.Specifications.EventEntitiesSpecs
{
    public class EventWithIncludesSpecifications : BaseSpecifications<Event>
    {
        public EventWithIncludesSpecifications(EventSpeceficationsParams speceficationsParams) :
            base(p =>
              (!speceficationsParams.categoryId.HasValue || p.EventCategoryId == speceficationsParams.categoryId.Value)
            &&
            (string.IsNullOrEmpty(speceficationsParams.Search)
              || p.Title.ToLower().Contains(speceficationsParams.Search)
               && p.Country.ToLower().Contains(speceficationsParams.Search)
            ))

        {
          
                AddIncludes();

                if (!string.IsNullOrEmpty(speceficationsParams.sort))
                {
                    switch (speceficationsParams.sort)
                    {

                        case "StartingDateAsc":
                            AddOrderBy(p => p.StartDate);
                            break;
                        case "StartingDateDesc":
                             AddOrderBy(p => p.StartDate);
                            break;

                        default:
                            AddOrderByDesc(p => p.StartDate);
                            break;
                    }
                }
                else
                    AddOrderByDesc(p => p.StartDate);

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

        }

    }
}
