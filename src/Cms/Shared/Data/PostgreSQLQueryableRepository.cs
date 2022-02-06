using Npgsql;
using System.Data.Common;

namespace Cms.Shared
{
    public abstract class PostgreSQLQueryableRepository<T, TId> : IDisposable, IQueryableRepos<T, TId> where T : IEntity<TId>
    {
        private NpgsqlConnection? _connection;
        private object _locker = new object();
        private DbTransaction _transaction;

        protected IConfiguration Configuration {  get; }

        protected DbTransaction Transaction => _transaction;

        protected PostgreSQLQueryableRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public abstract Task<T> Create(T item);


        public abstract Task<bool> Delete(TId item);


        public abstract Task<T> Get(TId id);


        public virtual async Task<IQueryResult<ICollection<T>>> QueryforMany(IEntityCollectionQuery<T, TId> queryProvider)
        {
            var conn = GetConnection();
            return await queryProvider.Query(conn, new List<object>());
            
        }

        public async  Task<IQueryResult<ICollection<T>>> QueryforMany<TArg>(IEntityCollectionQuery<T, TId> queryProvider, IEnumerable<TArg> args)
        {
            var conn = GetConnection();
            return await queryProvider.Query(conn,args);
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
                var connString = $"Server={configSection["host"]};Port=5432;Database=evolvit;User Id={configSection["User"]};Password={configSection["Password"]};";
                _connection = new NpgsqlConnection(connString);
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

        public void With(DbConnection connection, DbTransaction transaction = null)
        {
            lock (_locker)
            {
                if (connection == null || connection.GetType() != typeof(NpgsqlConnection))
                {
                    throw new ArgumentException($"connection cannot be null and must type {nameof(NpgsqlConnection)}");
                }
                _connection = (NpgsqlConnection)connection;
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
