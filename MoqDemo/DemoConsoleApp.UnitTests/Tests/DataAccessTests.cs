using DemoConsoleApp.Data;
using DemoConsoleApp.Helpers;
using Moq;
using Moq.Protected;

namespace DemoConsoleApp.UnitTests
{
    public class DataAccessTests
    {
        [Fact]
        public void IsPayloadExists_ExistingPayload_ReturnsTrue()
        {
            // Arrange
            var requestId = "12345";
            var payload = new Mock<Payload>();

            var mockConnectionWrapper = new Mock<ISQLiteConnection>();
            mockConnectionWrapper.Setup(c => c.Open());
            mockConnectionWrapper.Object.Open();


            var mockCommand = new Mock<SQLiteCommandWrapper>() { CallBase = true };
            mockCommand.Setup(c => c.ParametersAddWithValue("@RequestId", requestId));
            mockCommand.Setup(c => c.ExecuteScalar()).Returns(1);

            var mockDataAccess = new Mock<ISQLiteDataAccess>() { CallBase = true };
            mockCommand.Setup(c => c.ExecuteScalar()).Returns(true);
            mockDataAccess.Protected().Setup("InsertPayload", payload.Object);
            mockCommand.Object.ExecuteScalar();

            // Act
            mockDataAccess.Setup(x => x.IsPayloadExists(requestId)).Returns(true);
            var result = mockDataAccess.Object.IsPayloadExists(requestId);

            // Assert
            Assert.True(result);
            mockConnectionWrapper.Verify(c => c.Open(), Times.Once);
            mockCommand.Verify(c => c.ExecuteScalar(), Times.Once);
        }

        [Fact]
        public void IsPayloadExists_NonExistingPayload_ReturnsFalse()
        {
            // Arrange
            var requestId = "56789";
            var mockConnectionWrapper = new Mock<ISQLiteConnection>();
            mockConnectionWrapper.Setup(c => c.Open());
            mockConnectionWrapper.Object.Open();

            var mockCommand = new Mock<SQLiteCommandWrapper>(mockConnectionWrapper.Object) { CallBase = true };
            mockCommand.Setup(c => c.ParametersAddWithValue("@RequestId", requestId));
            mockCommand.Setup(c => c.ExecuteScalar()).Returns(0);
            mockCommand.Object.ExecuteScalar();

            var mockDataAccess = new Mock<ISQLiteDataAccess>() { CallBase = true };
            mockDataAccess.Setup(d => d.IsPayloadExists(requestId)).Returns(false);
            mockDataAccess.Protected().Setup("CreateCommand");

            // Act
            var result = mockDataAccess.Object.IsPayloadExists(requestId);

            // Assert
            Assert.False(result);
            mockConnectionWrapper.Verify(c => c.Open(), Times.Once);
            mockCommand.Verify(c => c.ExecuteScalar(), Times.Once);
        }
    }
}
