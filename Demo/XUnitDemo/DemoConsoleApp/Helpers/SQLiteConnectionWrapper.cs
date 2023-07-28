using System.Data.SQLite;

namespace DemoConsoleApp.Helpers
{
    public class SQLiteConnectionWrapper : ISQLiteConnectionWrapper
    {
        private readonly SQLiteConnection _connection;

        public SQLiteConnectionWrapper(string connectionString)
        {
            _connection = new SQLiteConnection(connectionString);
        }

        public void Open()
        {
            _connection.Open();
        }

        public ISQLiteCommandWrapper CreateCommand()
        {
            return new SQLiteCommandWrapper(_connection.CreateCommand());
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public interface ISQLiteConnectionWrapper : IDisposable
    {
        void Open();
        ISQLiteCommandWrapper CreateCommand();
    }
}
