using Cms.Shared;

namespace Cms.ContentTemplate
{
    public class ContentTemplateAggregate : IEntity<int>
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public IEnumerable<ContentField> ContentFields { get; set; } = new List<ContentField>();
    }
}
