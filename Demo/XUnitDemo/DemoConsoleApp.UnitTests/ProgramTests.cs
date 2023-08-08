using DemoConsoleApp.Data;
using DemoConsoleApp.Helpers;
using Microsoft.Extensions.Configuration;
using Moq;

namespace DemoConsoleApp.UnitTests
{
    public class ProgramTests
    {
        [Fact]
        public void Main_ShouldWriteHelloWorld()
        {
            // Arrange
            var expectedOutput = "Hello, Sign Team!";
            var consoleOutput = new ConsoleOutput();

            // Act
            Program.Main(new string[] { "payloads.db" });

            // Assert
            Assert.Contains(expectedOutput, consoleOutput.GetOutput());
        }

        [Fact]
        public void Main_Should_Print_Record_Count()
        {
            // Arrange
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration.Setup(c => c["AppSettings:ConnectionString"]).Returns("mock_connection_string");

            var mockDataAccess = new Mock<ISQLiteDataAccess>();
            mockDataAccess.Setup(d => d.GetAllPayloads()).Returns(new List<Payload>());

            // Arrange
            var consoleOutput = new ConsoleOutput();

            // Act
            Program.Main(new string[] { "payloads.db" });
            var console = consoleOutput.GetOutput();

            // Assert
            Assert.Contains("Records Count: 2", console);
        }


    }

    public class ConsoleOutput : IDisposable
    {
        private readonly StringWriter _stringWriter;
        private readonly TextWriter _originalOutput;

        public ConsoleOutput()
        {
            _stringWriter = new StringWriter();
            _originalOutput = Console.Out;
            Console.SetOut(_stringWriter);
        }

        public string GetOutput()
        {
            return _stringWriter.ToString().Trim();
        }

        public void Dispose()
        {
            Console.SetOut(_originalOutput);
            _stringWriter.Dispose();
        }
    }
}