namespace Cms.Shared
{
    public interface IEntity<TId>
    {
        TId Id {  get; set; }
    }
}
