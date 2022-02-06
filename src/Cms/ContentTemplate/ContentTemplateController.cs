using Cms.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cms.ContentTemplate
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentTemplateController : ControllerBase
    {
        private IContentTemplateManager _manager;

        public ContentTemplateController(IContentTemplateManager manager)
        {
           _manager = manager;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var result = await _manager.Load(id);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            var result = await _manager.LoadPaged(pageNumber, pageSize);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ContentTemplateAggregate template)
        {
            var result = await _manager.CreateTemplate(template);
            return Created($"api/contenttemplate/{result.Id}", result);
        }
    }
}
