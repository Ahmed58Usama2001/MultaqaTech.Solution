﻿namespace MultaqaTech.Core.Specifications.EventEntitiesSpecs.EventSpecs
{
    public class EventWithFilterationForCountSpecifications : BaseSpecifications<Event>
    {
        public EventWithFilterationForCountSpecifications(EventSpeceficationsParams speceficationsParams) :
            base(p =>
              (!speceficationsParams.categoryId.HasValue || p.EventCategoryId == speceficationsParams.categoryId.Value)
            &&
            (!speceficationsParams.countryId.HasValue || p.EventCountryId == speceficationsParams.countryId.Value)
            &&
            (string.IsNullOrEmpty(speceficationsParams.Search)
              || p.Title.ToLower().Contains(speceficationsParams.Search)
            ))


        {


        }
    }
}
