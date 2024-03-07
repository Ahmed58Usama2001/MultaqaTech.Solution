
namespace MultaqaTech.Core.Specifications.BlogPost_Specs;

public class BlogPostWithFilterationForCountSpecifications : BaseSpecifications<BlogPost>
    {
        public BlogPostWithFilterationForCountSpecifications(BlogPostSpeceficationsParams speceficationsParams) :
              base(p =>
            (string.IsNullOrEmpty(speceficationsParams.Search)
              || p.Title.ToLower().Contains(speceficationsParams.Search)
              || p.Content.ToLower().Contains(speceficationsParams.Search)
               || p.AuthorName.ToLower().Contains(speceficationsParams.Search) &&
            (!speceficationsParams.categoryId.HasValue || p.BlogPostCategoryId == speceficationsParams.categoryId.Value)
            ))
        {

        }
    }

