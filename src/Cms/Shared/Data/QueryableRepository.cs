
using MySqlConnector;
using System.Data.Common;

namespace Cms.Shared
{
    public abstract class QueryableRepository<T, TId> : IDisposable, IQueryableRepos<T, TId> where T : IEntity<TId>
    {
        private DbConnection? _connection;
        private object _locker = new object();
        private DbTransaction? _transaction;

        protected IConfiguration Configuration {  get; }

        protected DbTransaction? Transaction => _transaction;

        protected QueryableRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public abstract Task<T> Create(T item);

        
        public abstract Task<bool> Delete(TId item);


        public abstract Task<T?> Get(TId id);


        public virtual async Task<IQueryResult<ICollection<T>>> QueryforMany(IEntityCollectionQuery<T, TId> queryProvider)
        {
            var conn = GetConnection();
            return await queryProvider.Query(conn);
            
        }

        public virtual async Task<IQueryResult<IEntity<TId>>> QueryforSingle(IEntityQuery<T, TId> queryProvider)
        {
            var conn = GetConnection();
            return await queryProvider.Query(conn);
        }

        public abstract Task<T> Update(T item);

        public DbConnection GetConnection()
        {
           
            if(_connection != null)
            {
                return _connection;
            }
            lock (_locker)
            {
                var configSection = Configuration.GetSection("EVOLVIT");
                var connString = $"Server={configSection["host"]};Port=3306;Database=evolvit;User Id={configSection["User"]};Password={configSection["Password"]};";
                _connection = new MySqlConnection(connString);
                return _connection;
            }
          
        }

        public async Task<DbTransaction> WithTransaction()
        {
            if(_transaction == null)
            {
                var connection = GetConnection();
                _transaction = await connection.BeginTransactionAsync();
            }     
            return _transaction;
        }

        public void With(DbConnection connection, DbTransaction? transaction = null)
        {
            lock (_locker)
            {
                if (connection == null || connection.GetType() != typeof( MySqlConnection))
                {
                    throw new ArgumentException($"connection cannot be null and must type {nameof( MySqlConnection)}");
                }
                _connection = ( MySqlConnection)connection;
                if(transaction != null)
                {
                    _transaction = transaction;
                }
            }
        }

        public void Dispose()
        {
            if(_connection != null)
            {
                _connection.Dispose();
            }
        }

    }
}
