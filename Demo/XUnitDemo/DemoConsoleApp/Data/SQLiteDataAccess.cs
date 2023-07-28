using DemoConsoleApp.Data;
using DemoConsoleApp.Helpers;
using System.Data.SQLite;

namespace DemoConsoleApp
{
    public class SQLiteDataAccess
    {
        private readonly string _databasePath;

        public SQLiteDataAccess(string databaseName)
        {
            _databasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, databaseName);
        }

        public SQLiteConnection GetConnection()
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

        // Check if the payload with given RequestId exists
        public bool IsPayloadExists()
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var selectQuery = "SELECT COUNT(*) FROM Payloads LIMIT 1";
                using (var command = new SQLiteCommand(selectQuery, connection))
                {
                    var result = (long)command.ExecuteScalar();
                    return result > 0;
                }
            }
        }

        // Check if the payload with given RequestId exists
        public bool IsPayloadExists(string requestId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var selectQuery = "SELECT COUNT(*) FROM Payloads WHERE RequestId = @RequestId";
                using (var command = new SQLiteCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@RequestId", requestId);

                    var result = (long)command.ExecuteScalar();
                    return result > 0;
                }
            }
        }
        // Delete the payload with the given RequestId
        public void DeletePayload(string requestId)
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                var deleteQuery = "DELETE FROM Payloads WHERE RequestId = @RequestId";

                using (var command = new SQLiteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@RequestId", requestId);

                    command.ExecuteNonQuery();
                }
            }
        }

        // Get all payloads from the database
        public List<Payload> GetAllPayloads()
        {
            var payloads = new List<Payload>();

            using (var connection = GetConnection())
            {
                connection.Open();

                var selectQuery = "SELECT * FROM Payloads";

                using (var command = new SQLiteCommand(selectQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var payload = new Payload
                            {
                                RequestId = reader.GetString(0),
                                ClientId = reader.GetString(1),
                                File = reader.GetString(2),
                                Status = reader.GetString(3),
                                Message = reader.IsDBNull(4) ? null : reader.GetString(4),
                                TimeStamp = reader.GetDateTime(5)
                            };

                            payloads.Add(payload);
                        }
                    }
                }
            }

            return payloads;
        }



    }
}
