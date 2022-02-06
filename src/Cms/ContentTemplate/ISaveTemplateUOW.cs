using Cms.Shared;

namespace Cms.ContentTemplate
{
    public interface ISaveTemplateUOW : IUnitOfWork<ContentTemplateAggregate, int>
    {
    }
}
