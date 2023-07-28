using System.Data.SQLite;

namespace DemoConsoleApp.Helpers
{
    public interface ISQLiteConnectionWrapper : IDisposable
    {
        void Open();
        ISQLiteCommandWrapper CreateCommand();
        // Add other methods or properties as needed
    }

}
