namespace MultaqaTech.Core.Specifications.BlogPost_Specs;

public class BlogPostCommentWithIncludesSpecifications : BaseSpecifications<BlogPostComment>
{
    public BlogPostCommentWithIncludesSpecifications(BlogPostCommentSpeceficationsParams speceficationsParams)
        : base(p => (!speceficationsParams.blogPostId.HasValue || p.BlogPostId == speceficationsParams.blogPostId.Value))

    {
        AddIncludes();

        if(!string.IsNullOrEmpty(speceficationsParams.sort))
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

    public BlogPostCommentWithIncludesSpecifications(int id)
        :base(p=>p.Id==id)
    {
        AddIncludes();
    }

    private void AddIncludes()
    {
        Includes.Add(p => p.BlogPost);
    }

}
