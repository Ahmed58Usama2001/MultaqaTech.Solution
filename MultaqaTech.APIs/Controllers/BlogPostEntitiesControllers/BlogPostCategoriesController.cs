﻿namespace MultaqaTech.APIs.Controllers.BlogPostEntitiesControllers;

[Authorize]
public class BlogPostCategoriesController(IBlogPostCategoryService blogPostCategoryService, IMapper mapper) : BaseApiController
{
    private readonly IBlogPostCategoryService _blogPostCategoryService = blogPostCategoryService;
    private readonly IMapper _mapper = mapper;


    [ProducesResponseType(typeof(BlogPostCategory), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<ActionResult<BlogPostCategory>> CreateBlogPostCategory(BlogPostCategoryCreateDto blogPostCategoryDto)
    {
        if (blogPostCategoryDto is null) return BadRequest(new ApiResponse(400));

        var createdCategory = await _blogPostCategoryService.CreateBlogPostCategoryAsync(_mapper.Map<BlogPostCategory>(blogPostCategoryDto));

        if (createdCategory is null) return BadRequest(new ApiResponse(400));

        return Ok(createdCategory);
    }

    [ProducesResponseType(typeof(BlogPostCategory), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<BlogPostCategory>>> GetAllBlogPostCategories()
    {
        var categories = await _blogPostCategoryService.ReadAllAsync();

        if (categories == null)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        return Ok(categories);
    }


    [ProducesResponseType(typeof(BlogPostCategory), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpGet("{id}")]
    public async Task<ActionResult<BlogPostCategory>> GetBlogPostCategory(int id)
    {
        var category = await _blogPostCategoryService.ReadByIdAsync(id);

        if (category == null)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        return Ok(category);
    }

    [ProducesResponseType(typeof(BlogPostCategory), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpPut("{categoryId}")]
    public async Task<ActionResult<BlogPostCategory>> UpdateBlogPostCategory(int categoryId, BlogPostCategoryCreateDto updatedCategory)
    {
        var category = await _blogPostCategoryService.UpdateBlogPostCategory(categoryId, _mapper.Map<BlogPostCategory>(updatedCategory));

        if (category == null)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        return Ok(category);
    }

    [ProducesResponseType(typeof(BlogPostCategory), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<BlogPostCategory>> DeleteBlogPostCategory(int id)
    {
        var result = await _blogPostCategoryService.DeleteBlogPostCategory(id);

        if (!result)
            return NotFound(new { Message = "Not Found", StatusCode = 404 });

        return NoContent();
    }
}
