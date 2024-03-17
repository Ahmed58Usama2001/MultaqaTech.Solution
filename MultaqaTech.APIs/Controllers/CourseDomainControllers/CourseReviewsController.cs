namespace MultaqaTech.APIs.Controllers.CourseDomainControllers;

public partial class CoursesController : BaseApiController
{
    [ProducesResponseType(typeof(CourseReviewToReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpPost("Reviews")]
    public async Task<ActionResult<CourseReviewToReturnDto>> CreateCourseReview(CourseReviewDto reviewDto)
    {
        if (reviewDto is null) return BadRequest(new ApiResponse(400));

        string? studentEmail = User.FindFirstValue(ClaimTypes.Email);
        if (studentEmail is null) return NotFound(new ApiResponse(404));

        AppUser? student = await _userManager.FindByEmailAsync(studentEmail);
        if (student is null) return NotFound(new ApiResponse(404));

        CourseReview? createdCourseReview = await _courseService.CreateCourseReviewAsync(_mapper.Map<CourseReview>(reviewDto), student);

        if (createdCourseReview is null) return BadRequest(new ApiResponse(400));

        return Ok(_mapper.Map<CourseReviewToReturnDto>(createdCourseReview));
    }

    [ProducesResponseType(typeof(CourseReviewToReturnDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [HttpPut("Reviews/{reviewId}")]
    public async Task<ActionResult<CourseReviewToReturnDto>> UpdateCourseReview(int reviewId, CourseReviewDto reviewDto)
    {
        if (reviewDto is null) return BadRequest(new ApiResponse(400));

        CourseReview mappedCourseReview = _mapper.Map<CourseReview>(reviewDto);
        mappedCourseReview.Id = reviewId;

        CourseReview? createdCourseReview = await _courseService.UpdateCourseReviewAsync(mappedCourseReview);

        if (createdCourseReview is null) return BadRequest(new ApiResponse(400));

        return Ok(_mapper.Map<CourseReviewToReturnDto>(createdCourseReview));
    }

    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [HttpDelete("Reviews/{reviewId}")]
    public async Task<ActionResult<CourseReviewToReturnDto>> DeleteCourseReview(int reviewId, CourseReviewDto reviewDto)
    {
        if (reviewDto is null) return BadRequest(new ApiResponse(400));

        CourseReview mappedCourseReview = _mapper.Map<CourseReview>(reviewDto);
        mappedCourseReview.Id = reviewId;

        bool result = await _courseService.DeleteCourseReviewAsync(mappedCourseReview);

        if (!result) return NotFound(new ApiResponse(404));

        return NoContent();
    }
}