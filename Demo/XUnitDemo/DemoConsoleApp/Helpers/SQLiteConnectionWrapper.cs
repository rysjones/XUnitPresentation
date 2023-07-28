using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // Implement IDisposable and other methods as needed
    }

}
