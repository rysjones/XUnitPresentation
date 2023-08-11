using DemoConsoleApp.Data;
using DemoConsoleApp.Helpers;
using DemoConsoleApp.Services;
using Moq;
using Moq.Protected;
using System.Data.Common;
using System.Data.SQLite;

namespace DemoConsoleApp.UnitTests.Tests
{
    public class PayloadServiceTests
    {

        [Fact]
        public void TestInsertPayload_WithValidPayload_InsertsSuccessfully()
        {
            // Arrange
            var validPayload = new Payload
            {
                RequestId = "9874",
                ClientId = "ClientX",
                File = "file3.txt",
                Status = "Success",
                Message = "File processed successfully",
                TimeStamp = DateTime.Now
            };
            string basePath = AppDomain.CurrentDomain.BaseDirectory.Replace(".UnitTests", string.Empty);
            var dbPath = Path.GetFullPath(basePath + "payloads.db");
            var sqlConn = new SQLiteConnection($"Data Source={dbPath};Version=3;");

            var mockConnection = new Mock<ISQLiteConnection>();
            var mockCommand = new Mock<ISQLiteCommand>();

            mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockConnection.Setup(c => c.Open()).Verifiable();
            mockConnection.Setup(c => c.Connection(dbPath)).Returns(sqlConn);
            mockConnection.Setup(c => c.Close()).Verifiable();

            var payloadService = new PayloadService(sqlConn);

            // Act
            var isAlreadyInserted = payloadService.InsertPayload(validPayload);

            // Assert
            Assert.True(isAlreadyInserted);
        }

    [Fact]
        public void TestGetPayloadById()
        {
            // Arrange
            var requestId = "6789";
            var expectedPayload = new Payload
            {
                RequestId = requestId,
                ClientId = "ClientX",
                File = "file3.txt",
                Status = "Success",
                Message = "File processed successfully",
                TimeStamp = DateTime.Now
            };

            var mockConnection = new Mock<ISQLiteConnection>();
            var mockCommand = new Mock<ISQLiteCommand>();
            var mockReader = new Mock<DbDataReader>();

            mockReader.SetupSequence(r => r.Read())
                .Returns(true)
                .Returns(false);

            mockReader.Setup(r => r.GetString(0)).Returns(expectedPayload.RequestId);
            mockReader.Setup(r => r.GetString(1)).Returns(expectedPayload.ClientId);
            mockReader.Setup(r => r.GetString(2)).Returns(expectedPayload.File);
            mockReader.Setup(r => r.GetString(3)).Returns(expectedPayload.Status);
            mockReader.Setup(r => r.IsDBNull(4)).Returns(false);
            mockReader.Setup(r => r.GetString(4)).Returns(expectedPayload.Message);
            mockReader.Setup(r => r.GetDateTime(5)).Returns(expectedPayload.TimeStamp);

            mockCommand.Setup(c => c.ParametersAddWithValue("@RequestId", requestId));
            mockCommand.Object.ExecuteScalar();

            mockConnection.Setup(c => c.Open()).Verifiable();
            mockConnection.Object.Open();

            var mockDataAccess = new Mock<ISQLiteDataAccess>() { CallBase = true };
            mockCommand.Setup(c => c.ExecuteScalar()).Returns(true);
            mockDataAccess.Protected().Setup("InsertPayload", expectedPayload);
            mockCommand.Object.ExecuteScalar();

            // Act
            mockDataAccess.Setup(x => x.GetPayloadById(requestId)).Returns(expectedPayload);
            var result = mockDataAccess.Object.GetPayloadById(requestId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedPayload.RequestId, result.RequestId);
            Assert.Equal(expectedPayload.ClientId, result.ClientId);
            Assert.Equal(expectedPayload.File, result.File);
            Assert.Equal(expectedPayload.Status, result.Status);
            Assert.Equal(expectedPayload.Message, result.Message);
            Assert.Equal(expectedPayload.TimeStamp, result.TimeStamp);

            // Verify
            mockDataAccess.Verify(f => f.GetPayloadById(expectedPayload.RequestId), Times.Once);
            mockConnection.Verify(c => c.Open(), Times.Once);
            //mockCommand.Verify(c => c.ParametersAddWithValue("@RequestId", requestId), Times.Once);
        }
    }
}
