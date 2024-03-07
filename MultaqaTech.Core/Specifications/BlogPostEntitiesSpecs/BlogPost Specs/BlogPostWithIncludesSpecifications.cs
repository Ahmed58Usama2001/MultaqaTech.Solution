
namespace MultaqaTech.Core.Specifications.BlogPost_Specs;

public class BlogPostWithIncludesSpecifications : BaseSpecifications<BlogPost>
{
    public BlogPostWithIncludesSpecifications(BlogPostSpeceficationsParams speceficationsParams)
        :  base(p =>
            (string.IsNullOrEmpty(speceficationsParams.Search)
              || p.Title.ToLower().Contains(speceficationsParams.Search)
              || p.Content.ToLower().Contains(speceficationsParams.Search)
               || p.AuthorName.ToLower().Contains(speceficationsParams.Search) &&
            (!speceficationsParams.categoryId.HasValue || p.BlogPostCategoryId == speceficationsParams.categoryId.Value)
            ))
        
    {
        AddIncludes();

        if(!string.IsNullOrEmpty(speceficationsParams.sort))
        {
            switch (speceficationsParams.sort)
            {
                case "NumberOfViewsAsc":
                    AddOrderBy(p => p.NumberOfViews);
                    break;

                case "NumberOfViewsDesc":
                    AddOrderByDesc(p => p.NumberOfViews);
                    break;

                case "PublishingDateAsc":
                    AddOrderBy(p => p.PublishingDate);
                    break;

                default:
                    AddOrderByDesc(p => p.PublishingDate);
                    break;
            }
        }
        else
            AddOrderByDesc(p => p.PublishingDate);

        ApplyPagination((speceficationsParams.PageIndex - 1) * speceficationsParams.PageSize, speceficationsParams.PageSize);
    }

    public BlogPostWithIncludesSpecifications(int id)
        :base(p=>p.Id==id)
    {
        AddIncludes();
    }

    private void AddIncludes()
    {
        Includes.Add(p => p.Category);
        Includes.Add(p => p.Comments);
        Includes.Add(p => p.Tags);
    }

}
