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

        public void Close()
        {
            _connection.Close();
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        ISQLiteCommand ISQLiteConnection.CreateCommand()
        {
            ISQLiteCommand cmd = new SQLiteCommandWrapper();
            return cmd;
        }

        SQLiteConnection ISQLiteConnection.Connection(string connectionString)
        {
            return new SQLiteConnection(connectionString);
        }
    }

    public interface ISQLiteConnection : IDisposable
    {
        void Open();
        void Close();
        ISQLiteCommand CreateCommand();
        SQLiteConnection Connection(string connectionString);
    }
}
