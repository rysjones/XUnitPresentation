namespace DemoConsoleApp.Data
{
    public class PaylodData
    {
        public static void InsertPayloadIfNotExists(string databasePath)
        {
            SQLiteDataAccess dataAccess = new SQLiteDataAccess(databasePath);
            if (dataAccess.IsPayloadExists())
            {
                // Data already exists, do not insert
                return;
            }
             
            dataAccess.InitializeDatabase();

            var payload1 = new Payload
            {
                RequestId = "12345",
                ClientId = "ClientA",
                File = "file1.txt",
                Status = "Success",
                Message = "File processed successfully",
                TimeStamp = DateTime.Now
            };

            var payload2 = new Payload
            {
                RequestId = "67890",
                ClientId = "ClientB",
                File = "file2.txt",
                Status = "Failed",
                Message = "Error occurred during processing",
                TimeStamp = DateTime.Now
            };

            dataAccess.InsertPayload(payload1);
            dataAccess.InsertPayload(payload2);
        }
    }
}
