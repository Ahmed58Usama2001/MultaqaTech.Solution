namespace MultaqaTech.APIs.Controllers;

[Authorize]
public class BlogPostsController(IBlogPostService blogPostService, IMapper mapper, UserManager<AppUser> userManager,IBlogPostCategoryService blogPostCategoryService ) : BaseApiController
{
    private readonly IBlogPostService _blogPostService = blogPostService;
    private readonly IMapper _mapper = mapper;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly IBlogPostCategoryService _blogPostCategoryService = blogPostCategoryService;
 

    [ProducesResponseType(typeof(BlogPostDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<BlogPostDto>> CreateBlogPostAsync(BlogPostDto blogPostDto)
    {
        if (blogPostDto is null) return BadRequest(new ApiResponse(400));

        var authorEmail = User.FindFirstValue(ClaimTypes.Email);
        if (authorEmail is null) return BadRequest(new ApiResponse(404));

        var user = await _userManager.FindByEmailAsync(authorEmail);
        if (user is null) return BadRequest(new ApiResponse(404));

        blogPostDto.PublishingDate = DateTime.Now.ToString("dddd, MMMM dd, yyyy 'at' hh:mm:ss tt");

        // Assuming you have a service or repository to fetch the category by ID
        var existingCategory =await _blogPostCategoryService.ReadByIdAsync(blogPostDto.CategoryId);

        var mappedblogPost = new BlogPost
        {
            Title = blogPostDto.Title,
            AuthorName = user.UserName,
            Content = blogPostDto.Content,
            CategoryId = blogPostDto.CategoryId,
            Category = existingCategory, 
            PublishingDate = DateTime.Now, 
            NumberOfViews = blogPostDto.NumberOfViews,
            Tags = MapTags(blogPostDto.Tags), 
            Comments = MapComments(blogPostDto.Comments) 
        };

        var createdBlogPost = await blogPostService.CreateBlogPostAsync(mappedblogPost);

        if (createdBlogPost is null) return BadRequest(new ApiResponse(400));


        return Ok(_mapper.Map<BlogPost, BlogPostDto>(createdBlogPost));
    }



    [ProducesResponseType(typeof(BlogPostDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<ActionResult<Pagination<BlogPostDto>>> GetBlogPosts([FromQuery] BlogPostSpeceficationsParams speceficationsParams)
    {
        var blogPosts = await _blogPostService.ReadBlogPostsAsync(speceficationsParams);

        if (blogPosts == null)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        var data = _mapper.Map<IReadOnlyList<BlogPost>, IReadOnlyList<BlogPostDto>>(blogPosts);


        var count = await _blogPostService.GetCountAsync(speceficationsParams);

        return Ok(new Pagination<BlogPostDto>(speceficationsParams.PageIndex, speceficationsParams.PageSize, count, data));
    }

    [ProducesResponseType(typeof(BlogPostDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<BlogPostDto>> GetBlogPost(int id)
    {
        await _blogPostService.IncrementViewCountAsync(id);

        var blogPost = await _blogPostService.ReadByIdAsync(id);

        if (blogPost == null)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        return Ok(_mapper.Map<BlogPost, BlogPostDto>(blogPost));
    }

    [ProducesResponseType(typeof(BlogPost), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpPut]
    public async Task<ActionResult<Subject>> UpdateBlogPost(int blogPostId, BlogPost updatedBlogPost)
    {
        var blogPost = await _blogPostService.UpdateBlogPost(blogPostId, updatedBlogPost);

        if (blogPost == null)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        return Ok(_mapper.Map<BlogPost, BlogPostDto>(blogPost));
    }


    [ProducesResponseType(typeof(BlogPost), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpDelete]
    public async Task<ActionResult<BlogPost>> DeleteBlogPost(int blogPostId)
    {
        var result = await _blogPostService.DeleteBlogPost(blogPostId);

        if (!result)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        return Ok("The post was deleted Successfully");
    }


    private List<Subject> MapTags(List<string>? tags)
    {
        if (tags == null)
            return null;

        return tags.Select(tag => new Subject { Name = tag }).ToList();
    }


    private List<BlogPostComment> MapComments(List<string>? comments)
    {
        if (comments == null)
            return null;

        return comments.Select(comment => new BlogPostComment { CommentContent = comment }).ToList();
    }


}
