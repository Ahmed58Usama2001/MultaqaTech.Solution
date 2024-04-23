
namespace MultaqaTech.Core.Specifications.ZoomMeetingEntitiesSpecs
{
    public class ZoomMeetingWithFilterationForCountSpecifications : BaseSpecifications<ZoomMeeting>
    {
        public ZoomMeetingWithFilterationForCountSpecifications(ZoomMeetingSpeceficationsParams speceficationsParams) :
              base(p =>
              (!speceficationsParams.categoryId.HasValue || p.ZoomMeetingCategoryId == speceficationsParams.categoryId.Value)
            &&
            (string.IsNullOrEmpty(speceficationsParams.Search)
              || p.Title.ToLower().Contains(speceficationsParams.Search)
               || p.AuthorName.ToLower().Contains(speceficationsParams.Search)
            ))
        {

        }
    }
}
