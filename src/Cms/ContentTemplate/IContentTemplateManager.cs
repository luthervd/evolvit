using Cms.Shared;

namespace Cms.ContentTemplate
{
    public interface IContentTemplateManager
    {
        Task<ContentTemplateAggregate> Load(int id);
        Task<PagedResult<ContentTemplateAggregate>> LoadPaged(int page, int pageSize);

        Task<ContentTemplateAggregate> CreateTemplate(ContentTemplateAggregate template);
    }
}