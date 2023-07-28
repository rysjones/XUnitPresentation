using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsoleApp.Helpers
{
    public class SQLiteCommandWrapper : ISQLiteCommandWrapper
    {
        private readonly SQLiteCommand _command;

        public SQLiteCommandWrapper(SQLiteCommand command)
        {
            _command = command;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public object ExecuteScalar()
        {
            return (long)_command.ExecuteScalar();
        }

        public void ParametersAddWithValue(string paramName, object value)
        {
            _command.Parameters.AddWithValue(paramName, value);
        }

        // Implement IDisposable and other methods as needed
    }

}
