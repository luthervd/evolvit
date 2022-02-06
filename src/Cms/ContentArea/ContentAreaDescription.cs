using Cms.Shared;

namespace Cms.ContentArea
{
    public class ContentAreaDescription : IEntity<Guid>
    {
        public Guid Id { get; set;  }

        public string? Name { get; set;  }

        public string? Description { get; set;  }
       
    }
}
