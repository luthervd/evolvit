namespace Cms.Content
{
    public interface IContentService
    {
        Task<ContentData> Create(ContentData contentData); 

        Task<ContentData?> Get(Guid id);

        Task<ICollection<ContentData>> GetPaged(int pageNumber, int pageSize);

        Task<ContentData?> GetContent(Guid id);

        Task<ContentData?> Update(ContentData contentData);
    }
}
