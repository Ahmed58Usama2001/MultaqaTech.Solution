
namespace MultaqaTech.Core.Specifications.EventEntitiesSpecs.EventCommentSpecs
{
    public class EventCommentWithIncludesSpecifications : BaseSpecifications<EventComment>
    {
        public EventCommentWithIncludesSpecifications(EventCommentSpeceficationsParams speceficationsParams)
            : base(p => (!speceficationsParams.EventId.HasValue || p.EventId == speceficationsParams.EventId.Value))
        {
            AddIncludes();

            if (!string.IsNullOrEmpty(speceficationsParams.sort))
            {
                switch (speceficationsParams.sort)
                {
                    case "DatePostedAsc":
                        AddOrderBy(p => p.DatePosted);
                        break;

                    case "DatePostedDesc":
                        AddOrderByDesc(p => p.DatePosted);
                        break;

                    default:
                        AddOrderBy(p => p.DatePosted);
                        break;
                }
            }
            else
                AddOrderBy(p => p.DatePosted);

            ApplyPagination((speceficationsParams.PageIndex - 1) * speceficationsParams.PageSize, speceficationsParams.PageSize);
        }

        public EventCommentWithIncludesSpecifications(int id)
         :base(p => p.Id == id)
        { 
                AddIncludes();
            
        }

        private void AddIncludes()
        {
            Includes.Add(p => p.Event);
        }
    }
    
}
