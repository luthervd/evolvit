using Cms.Shared;

namespace Cms.ContentTemplate
{
    public class ContentField : IEntity<int>
    {
        public string? Name { get; set; }

        public string? Type { get; set; }
        
        public int Id { get; set; }

        public int ContentTemplateId { get; set; }
    }
}
