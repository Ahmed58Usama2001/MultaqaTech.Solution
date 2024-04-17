
namespace MultaqaTech.APIs.Controllers.BlogPostEntitiesControllers;

[Authorize]
public class BlogPostsController(IBlogPostService blogPostService, IMapper mapper, UserManager<AppUser> userManager, IBlogPostCategoryService blogPostCategoryService
    ,ISubjectService subjectService,IUnitOfWork unitOfWork) : BaseApiController
{
    private readonly IBlogPostService _blogPostService = blogPostService;
    private readonly IMapper _mapper = mapper;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly IBlogPostCategoryService _blogPostCategoryService = blogPostCategoryService;
    private readonly ISubjectService _subjectService = subjectService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;


    [ProducesResponseType(typeof(BlogPostToReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<BlogPostToReturnDto>> CreateBlogPostAsync(BlogPostCreateDto blogPostDto)
    {
        if (blogPostDto is null) return BadRequest(new ApiResponse(400));

        var authorEmail = User.FindFirstValue(ClaimTypes.Email);
        if (authorEmail is null) return BadRequest(new ApiResponse(404));

        var user = await _userManager.FindByEmailAsync(authorEmail);
        if (user is null) return BadRequest(new ApiResponse(404));

       var existingCategory = await _blogPostCategoryService.ReadByIdAsync(blogPostDto.CategoryId);
        if (existingCategory is null)
            return NotFound(new { Message = "Category wasn't Not Found", StatusCode = 404 });

        var mappedblogPost = new BlogPost
        {
            Title = blogPostDto.Title,
            AuthorName = user.UserName,
            Content = blogPostDto.Content,
            PostPictureUrl = blogPostDto.PictureUrl,
            BlogPostCategoryId = blogPostDto.CategoryId,
            Category = existingCategory,
            PublishingDate = DateTime.Now,
            NumberOfViews = 0,
            Tags = blogPostDto.Tags != null ? await MapTagsAsync(blogPostDto.Tags) : new List<Subject>()
        };

        var createdBlogPost = await blogPostService.CreateBlogPostAsync(mappedblogPost);

        if (createdBlogPost is null) return BadRequest(new ApiResponse(400));

        return Ok(_mapper.Map<BlogPost, BlogPostToReturnDto>(createdBlogPost));
    }



    [ProducesResponseType(typeof(BlogPostToReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<ActionResult<Pagination<BlogPostToReturnDto>>> GetBlogPosts([FromQuery] BlogPostSpeceficationsParams speceficationsParams)
    {
        var blogPosts = await _blogPostService.ReadBlogPostsAsync(speceficationsParams);

        if (blogPosts == null)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        var count = await _blogPostService.GetCountAsync(speceficationsParams);

        var data = _mapper.Map<IReadOnlyList<BlogPost>, IReadOnlyList<BlogPostToReturnDto>>(blogPosts);

        return Ok(new Pagination<BlogPostToReturnDto>(speceficationsParams.PageIndex, speceficationsParams.PageSize, count, data));
    }

    [ProducesResponseType(typeof(BlogPostToReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<BlogPostToReturnDto>> GetBlogPost(int id)
    {
        var blogPost = await _blogPostService.ReadByIdAsync(id);

        if (blogPost == null)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        await _blogPostService.IncrementViewCountAsync(id);

        return Ok(_mapper.Map<BlogPostToReturnDto>(blogPost));
    }

    [ProducesResponseType(typeof(BlogPostToReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpPut("{blogPostId}")]
    public async Task<ActionResult<BlogPostToReturnDto>> UpdateBlogPost(int blogPostId, BlogPostCreateDto updatedBlogPostDto)
    {
        var updatedPost = await _blogPostService.ReadByIdAsync(blogPostId);

        updatedPost.Title = updatedBlogPostDto.Title;

        updatedPost.Content = updatedBlogPostDto.Content;

        updatedPost.PostPictureUrl = updatedBlogPostDto.PictureUrl;

        updatedPost.BlogPostCategoryId = updatedBlogPostDto.CategoryId;
        var existingCategory = await _blogPostCategoryService.ReadByIdAsync(updatedBlogPostDto.CategoryId);
        if (existingCategory is null)
            return NotFound(new { Message = "Category wasn't Not Found", StatusCode = 404 });
        updatedPost.Category = existingCategory;

        updatedPost.Tags = updatedBlogPostDto.Tags != null ? await MapTagsAsync(updatedBlogPostDto.Tags) : new List<Subject>();

        var blogPost = await _blogPostService.UpdateBlogPost(blogPostId, updatedPost);

        if (blogPost == null)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        return Ok(_mapper.Map<BlogPostToReturnDto>(blogPost));
    }


    [ProducesResponseType(typeof(BlogPostToReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<BlogPostToReturnDto>> DeleteBlogPost(int id)
    {
        var blogPost = await _unitOfWork.Repository<BlogPost>().GetByIdAsync(id);
        if (blogPost == null)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        var authorEmail = User.FindFirstValue(ClaimTypes.Email);
        if (authorEmail is null) return BadRequest(new ApiResponse(404));

        var user = await _userManager.FindByEmailAsync(authorEmail);
        if (user is null || user.UserName != blogPost.AuthorName)
            return BadRequest(new ApiResponse(401));

        var result = await _blogPostService.DeleteBlogPost(blogPost);

        if (!result)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        return NoContent();
    }

    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{postId}/sharelink")]
    public async Task<IActionResult> GetShareableLink(int postId)
    {
        var sharedPost =await GetBlogPost(postId);

        if (sharedPost == null)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });
       
        var shareableLink = Url.ActionLink("GetBlogPost", "BlogPosts", new { id = postId }, Request.Scheme);

        return Ok(new { shareLink = shareableLink });
    }


    private async Task<List<Subject>> MapTagsAsync(List<int> tagsIds)
    {
        var tags = new List<Subject>();

        var fetchedTags = await _subjectService.ReadSubjectsByIds(tagsIds);

        foreach (var fetchedTag in fetchedTags)        
            tags.Add(fetchedTag);
        
        return tags;
    }

}
