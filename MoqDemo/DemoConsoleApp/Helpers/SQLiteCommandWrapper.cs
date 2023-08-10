using System.Data.SQLite;

namespace DemoConsoleApp.Helpers
{
    public class SQLiteCommandWrapper : ISQLiteCommand
    {
        private readonly SQLiteCommand _command;

        public SQLiteCommandWrapper(SQLiteCommand command)
        {
            _command = command;
        }

        public SQLiteCommandWrapper()
        {
            _command = new SQLiteCommand();
        }

        public SQLiteCommandWrapper(ISQLiteConnection sQLiteConnection)
        {
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public virtual object ExecuteScalar()
        {
            return (long)_command.ExecuteScalar();
        }

        public virtual void ParametersAddWithValue(string paramName, object value)
        {
            _command.Parameters.AddWithValue(paramName, value);
        }
    }

    public interface ISQLiteCommand : IDisposable
    {
        object ExecuteScalar();
        void ParametersAddWithValue(string paramName, object value);
    }
}
