using Microsoft.AspNetCore.Mvc;

namespace Cms.Content
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private IContentService _contentService;

        public ContentController(IContentService contentService)
        {
            _contentService = contentService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContentData content)
        {
            var result = await _contentService.Create(content);
            return Created($"/Content/{result.Id}", result);
        }

        [HttpGet]
        public async Task<IActionResult> GetPaged([FromQuery]int pageSize,[FromQuery]int pageNumber)
        {
            var result = await _contentService.GetPaged(pageNumber, pageSize);
            return Ok(result);
        }

       
    }
}
