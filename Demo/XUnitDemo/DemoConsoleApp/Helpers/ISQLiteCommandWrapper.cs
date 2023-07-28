namespace DemoConsoleApp.Helpers
{
    public interface ISQLiteCommandWrapper : IDisposable
    {
        object ExecuteScalar();
        void ParametersAddWithValue(string paramName, object value);
        // Add other methods or properties as needed
    }
}
