using Cms.Shared;

namespace Cms.Content
{
    public record ContentData : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public ICollection<string> Tags { get; set;  } = new List<string>();

        public IEnumerable<CmsContentField>  Data { get; set; } = new List<CmsContentField>();

    }
}
