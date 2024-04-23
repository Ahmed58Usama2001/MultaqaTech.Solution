namespace MultaqaTech.APIs.Controllers.BlogPostEntitiesControllers;

[Authorize]
public class BlogPostCommentsController(IBlogPostService blogPostService, IMapper mapper, UserManager<Core.Entities.Identity.AppUser> userManager, IBlogPostCommentService blogPostCommentService
    ,IUnitOfWork unitOfWork) : BaseApiController
{
    private readonly IBlogPostService _blogPostService = blogPostService;
    private readonly IMapper _mapper = mapper;
    private readonly UserManager<Core.Entities.Identity.AppUser> _userManager = userManager;
    private readonly IBlogPostCommentService _blogPostCommentService = blogPostCommentService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    [ProducesResponseType(typeof(BlogPostCommentToReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<BlogPostCommentToReturnDto>> CreateBlogPostAsync(BlogPostCommentCreateDto blogPostCommentDto)
    {
        if (blogPostCommentDto is null) return BadRequest(new ApiResponse(400));

        var authorEmail = User.FindFirstValue(ClaimTypes.Email);
        if (authorEmail is null) return BadRequest(new ApiResponse(404));

        var user = await _userManager.FindByEmailAsync(authorEmail);
        if (user is null) return BadRequest(new ApiResponse(404));

        var existingPost = await _blogPostService.ReadByIdAsync(blogPostCommentDto.BlogPostId);
        if (existingPost is null)
            return NotFound(new { Message = "Post wasn't Not Found", StatusCode = 404 });

        var mappedblogPostComment = new BlogPostComment
        {
            AuthorName = user.UserName,
            CommentContent = blogPostCommentDto.CommentContent,
            BlogPostId = blogPostCommentDto.BlogPostId,
            BlogPost = existingPost,
            DatePosted = DateTime.Now,
        };

        var createdBlogPostComment = await blogPostCommentService.CreateBlogPostAsync(mappedblogPostComment);

        if (createdBlogPostComment is null) return BadRequest(new ApiResponse(400));

        return Ok(_mapper.Map<BlogPostComment, BlogPostCommentToReturnDto>(createdBlogPostComment));
    }



    [ProducesResponseType(typeof(BlogPostCommentToReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<ActionResult<Pagination<BlogPostCommentToReturnDto>>> GetBlogPostComments([FromQuery] BlogPostCommentSpeceficationsParams speceficationsParams)
    {
        var blogPostComments = await _blogPostCommentService.ReadBlogPostCommentsAsync(speceficationsParams);

        if (blogPostComments == null)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        var count = await _blogPostCommentService.GetCountAsync(speceficationsParams);

        var data = _mapper.Map<IReadOnlyList<BlogPostComment>, IReadOnlyList<BlogPostCommentToReturnDto>>(blogPostComments);

        return Ok(new Pagination<BlogPostCommentToReturnDto>(speceficationsParams.PageIndex, speceficationsParams.PageSize, count, data));
    }

    [ProducesResponseType(typeof(BlogPostCommentToReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<BlogPostCommentToReturnDto>> GetBlogPostComment(int id)
    {
        var blogPostComment = await _blogPostCommentService.ReadByIdAsync(id);

        if (blogPostComment == null)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        return Ok(_mapper.Map<BlogPostCommentToReturnDto>(blogPostComment));
    }

    [ProducesResponseType(typeof(BlogPostCommentToReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpPut("{blogPostCommentId}")]
    public async Task<ActionResult<BlogPostCommentToReturnDto>> UpdateBlogPostComment(int blogPostCommentId, BlogPostCommentCreateDto updatedBlogPostCommentDto)
    {
        var updatedComment = await _blogPostCommentService.ReadByIdAsync(blogPostCommentId);

        if (updatedComment == null)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        updatedComment.CommentContent = updatedBlogPostCommentDto.CommentContent;

        var blogPostComment = await _blogPostCommentService.UpdateBlogPostComment(blogPostCommentId, updatedComment);

        if (blogPostComment == null)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        return Ok(_mapper.Map<BlogPostCommentToReturnDto>(blogPostComment));
    }


    [ProducesResponseType(typeof(BlogPostCommentToReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<BlogPostCommentToReturnDto>> DeleteBlogPostComment(int id)
    {
        var blogPostComment = await _unitOfWork.Repository<BlogPostComment>().GetByIdAsync(id);
        if (blogPostComment == null)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });


        var authorEmail = User.FindFirstValue(ClaimTypes.Email);
        if (authorEmail is null) return BadRequest(new ApiResponse(404));

        var user = await _userManager.FindByEmailAsync(authorEmail);
        if (user is null || user.UserName!= blogPostComment.AuthorName) 
            return BadRequest(new ApiResponse(401));

        var result = await _blogPostCommentService.DeleteBlogPostComment(blogPostComment);

        if (!result)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        return NoContent();
    }
}
