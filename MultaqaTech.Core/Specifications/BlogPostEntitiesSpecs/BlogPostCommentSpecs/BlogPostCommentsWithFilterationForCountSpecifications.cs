namespace MultaqaTech.Core.Specifications.BlogPost_Specs;

public class BlogPostCommentsWithFilterationForCountSpecifications : BaseSpecifications<BlogPostComment>
    {
        public BlogPostCommentsWithFilterationForCountSpecifications(BlogPostCommentSpeceficationsParams speceficationsParams) :
              base(p =>(!speceficationsParams.blogPostId.HasValue || p.BlogPostId == speceficationsParams.blogPostId.Value))
        {

        }
    }

