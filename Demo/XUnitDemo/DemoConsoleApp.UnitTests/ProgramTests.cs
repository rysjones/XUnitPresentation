
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
            DemoConsoleApp.Program.Main(new string[] { });

            // Assert
            Assert.Equal(expectedOutput, consoleOutput.GetOutput());
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