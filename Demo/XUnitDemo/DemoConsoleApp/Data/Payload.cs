using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoConsoleApp.Data
{
    public class Payload
    {
        public string RequestId { get; set; }
        public string ClientId { get; set; }
        public string File { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    public class InitDB
    {
        public static void Start()
        {
            var databaseName = "payloads.db";
            var dataAccess = new SQLiteDataAccess(databaseName);
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
