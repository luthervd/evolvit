using System.Runtime.Serialization;

namespace Cms.Shared
{
    public class DbInsertException : Exception
    {
        public DbInsertException()
        {
        }

        public DbInsertException(string? message) : base(message)
        {
        }

        public DbInsertException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DbInsertException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
