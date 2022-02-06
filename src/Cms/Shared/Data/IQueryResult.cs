namespace Cms.Shared
{
    public interface IQueryResult<T>
    {
        T Result {  get;  }

        bool Success {  get;  }
    }
}
