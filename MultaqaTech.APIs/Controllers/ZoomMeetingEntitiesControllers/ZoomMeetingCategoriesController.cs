

namespace MultaqaTech.APIs.Controllers.ZoomMeetingEntitiesControllers
{
    public class ZoomMeetingCategoriesController(IZoomMeetingCategoryService zoomMeetingCategoryService, IMapper mapper) : BaseApiController
    {
        private readonly IZoomMeetingCategoryService _zoomMeetingCategoryService = zoomMeetingCategoryService;
        private readonly IMapper _mapper = mapper;

        [ProducesResponseType(typeof(ZoomMeetingCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult<ZoomMeetingCategory>> CreateSubject(ZoomMeetingCategoryCreateDto zoomMeetingCategoryDto)
        {
            if (zoomMeetingCategoryDto is null) return BadRequest(new ApiResponse(400));
            var createdCategory = await _zoomMeetingCategoryService.CreateZoomMeetingCategoryAsync(_mapper.Map<ZoomMeetingCategory>(zoomMeetingCategoryDto));

            if (createdCategory is null) return BadRequest(new ApiResponse(400));

            return Ok(createdCategory);
        }


        [ProducesResponseType(typeof(ZoomMeetingCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ZoomMeetingCategory>> GetSubject(int id)
        {
            var category = await _zoomMeetingCategoryService.ReadByIdAsync(id);

            if (category == null)
                return NotFound(new { Message = "Not Found", StatusCode = 404 });

            return Ok(category);
        }

        [ProducesResponseType(typeof(ZoomMeetingCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpPut]
        
        public async Task<ActionResult<ZoomMeetingCategory>> UpdateSubject(int categoryId, ZoomMeetingCategoryCreateDto updatedCategory)
        {
            var category = await _zoomMeetingCategoryService.UpdateZoomMeetingCategory(categoryId, _mapper.Map<ZoomMeetingCategory>(updatedCategory));
           
                if (category == null)
                   return NotFound(new { Message = "Not Found", StatusCode = 404 });

                return Ok(category);
        }

        [ProducesResponseType(typeof(ZoomMeetingCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ZoomMeetingCategory>> DeleteZoomMeetingCategory(int id)
        {
            var result = await _zoomMeetingCategoryService.DeleteZoomMeetingCategory(id);

            if (!result)
                return NotFound(new { Message = "Not Found", StatusCode = 404 });

            return NoContent();
        }








    }
}
