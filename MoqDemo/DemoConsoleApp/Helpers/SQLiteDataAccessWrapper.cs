using DemoConsoleApp.Data;

namespace DemoConsoleApp.Helpers
{
    public class SQLiteDataAccessWrapper : ISQLiteDataAccess
    {
        private readonly SQLiteDataAccess _sqliteDataAccess;

        public SQLiteDataAccessWrapper(SQLiteDataAccess sqliteDataAccess)
        {
            _sqliteDataAccess = sqliteDataAccess;
        }

        public List<Payload> GetAllPayloads()
        {
            try
            {
                return _sqliteDataAccess.GetAllPayloads();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Payload GetPayloadById(string requestId)
        {
            try
            {
                return _sqliteDataAccess.GetPayloadById(requestId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual bool InsertPayload(Payload payload)
        {
            try
            {
                _sqliteDataAccess.InsertPayload(payload);
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

        public bool IsPayloadExists(string requestId)
        {
            return _sqliteDataAccess.IsPayloadExists(requestId);
        }

        public virtual bool CreateCommand()
        {
            try
            {
                _sqliteDataAccess.InitializeDatabase();
            }
            catch (Exception)
            {
                throw;
            }
            return true;
        }

    }
    public interface ISQLiteDataAccess
    { 
        bool IsPayloadExists(string requestId);
        protected bool InsertPayload(Payload payload);
        protected bool CreateCommand();
        List<Payload> GetAllPayloads();
        Payload GetPayloadById(string requestId);
    }
}
