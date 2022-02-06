namespace Cms.Shared
{
    public class PagedResult<T>
    {
        public int PageNumber { get; set; }

        public int PageSize {  get; set; }

        public ICollection<T>? Result { get; set; }

        public int Count => Result?.Count ?? 0;
    }
}
