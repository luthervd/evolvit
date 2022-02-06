using Cms.Shared;

namespace Cms.ContentArea
{
    public interface IContentAreaService
    {
        Task<PagedResult<ContentAreaDescription>> GetPaged(int pageIndex, int pageSize);

        Task<ContentAreaDescription> Create(ContentAreaDescription description);
    }
}
