using Microsoft.AspNetCore.Mvc;

namespace Cms.ContentArea
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentAreaController : ControllerBase
    {
        private readonly IContentAreaService _contentAreaService;

        public ContentAreaController(IContentAreaService contentAreaService)
        {
            _contentAreaService = contentAreaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]int pageSize, [FromQuery]int pageNumber)
        {
           var result = await _contentAreaService.GetPaged(pageNumber,pageSize);
           return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ContentAreaDescription contentAreaDescription)
        {
            var result = await _contentAreaService.Create(contentAreaDescription);
            return Created($"/{contentAreaDescription.Id}", result);
        }

    }
}
