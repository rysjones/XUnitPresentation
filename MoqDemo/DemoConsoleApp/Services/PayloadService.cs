using DemoConsoleApp.Data;
using System.Data.SQLite;

namespace DemoConsoleApp.Services
{
    public class PayloadService: IPayloadService
    {
        private readonly SQLiteConnection _dbConnection;

        public PayloadService(SQLiteConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public bool InsertPayload(Payload payload)
        {
            if(IsPayloadExists(payload.RequestId)) return true;

            SQLiteDataAccess sQLiteDataAccess = new SQLiteDataAccess(_dbConnection);
            sQLiteDataAccess.InsertPayload(payload);
            return IsPayloadExists(payload.RequestId);
        }

        public bool IsPayloadExists(string requestId)
        {
            SQLiteDataAccess sQLiteDataAccess = new SQLiteDataAccess(_dbConnection);
            return sQLiteDataAccess.IsPayloadExists(requestId);
        }

    }

    public interface IPayloadService
    {
        bool InsertPayload(Payload payload);
        bool IsPayloadExists(string requestId);
    }
}
