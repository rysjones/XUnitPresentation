using DemoConsoleApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
    public interface ISQLiteDataAccess
    {
        bool IsPayloadExists(string requestId);
        protected bool InsertPayload(Payload payload);
        List<Payload> GetAllPayloads();
    }
}
