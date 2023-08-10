using System.Data.SQLite;

namespace DemoConsoleApp.Helpers
{
    public class SQLiteConnectionWrapper : ISQLiteConnection
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

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        ISQLiteCommand ISQLiteConnection.CreateCommand()
        {
            throw new NotImplementedException();
        }
    }

    public interface ISQLiteConnection : IDisposable
    {
        void Open();
        protected ISQLiteCommand CreateCommand();
    }
}
