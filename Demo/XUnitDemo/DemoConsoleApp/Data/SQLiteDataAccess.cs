using DemoConsoleApp.Data;
using System;
using System.Data.SQLite;
using System.IO;

namespace DemoConsoleApp
{
    public class SQLiteDataAccess
    {
        private readonly string _databasePath;

        public SQLiteDataAccess(string databaseName)
        {
            _databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, databaseName);
        }

        private SQLiteConnection GetConnection()
        {
            return new SQLiteConnection($"Data Source={_databasePath};Version=3;");
        }

        // Initialize the database and table if they don't exist
        public void InitializeDatabase()
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var createTableQuery = @"
                CREATE TABLE IF NOT EXISTS Payloads (
                    RequestId TEXT PRIMARY KEY,
                    ClientId TEXT NOT NULL,
                    File TEXT NOT NULL,
                    Status TEXT NOT NULL,
                    Message TEXT,
                    TimeStamp DATETIME NOT NULL
                )";

                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void InsertPayload(Payload payload)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var insertQuery = @"
                INSERT INTO Payloads (RequestId, ClientId, File, Status, Message, TimeStamp)
                VALUES (@RequestId, @ClientId, @File, @Status, @Message, @TimeStamp)";

                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@RequestId", payload.RequestId);
                    command.Parameters.AddWithValue("@ClientId", payload.ClientId);
                    command.Parameters.AddWithValue("@File", payload.File);
                    command.Parameters.AddWithValue("@Status", payload.Status);
                    command.Parameters.AddWithValue("@Message", payload.Message);
                    command.Parameters.AddWithValue("@TimeStamp", payload.TimeStamp);

                    command.ExecuteNonQuery();
                }
            }
        }

    }

}
