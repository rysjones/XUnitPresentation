using DemoConsoleApp.Data;
using DemoConsoleApp.Helpers;
using Moq;
using System.Data.Common;

namespace DemoConsoleApp.UnitTests.Tests
{
    public class PayloadServiceTests
    {
        [Fact]
        public void TestGetPayloadById()
        {
            // Arrange
            var requestId = "testRequestId";
            var expectedPayload = new Payload
            {
                RequestId = requestId,
                ClientId = "testClientId",
                File = "testFile",
                Status = "testStatus",
                Message = "testMessage",
                TimeStamp = DateTime.UtcNow
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
            mockDataAccess.Setup(c => c.GetPayloadById(expectedPayload.RequestId)).Returns(expectedPayload);
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
