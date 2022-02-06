namespace Cms.Shared
{
    public class QueryResult<T> : IQueryResult<T>
    {
        public QueryResult(T result, bool success = true)
        {
            Result = result;
            Success = success;
        }
        public T Result { get; private set; }

        public bool Success { get; private set; }
    }
}
