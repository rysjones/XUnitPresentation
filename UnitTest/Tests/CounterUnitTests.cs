namespace UnitTestingProject;
// dotnet test --filter FullyQualifiedName~UnitTestingProject.CounterUnitTests --logger "console;verbosity=detailed"

[TestCaseOrderer("UnitTestingProject.Orderers.PriorityOrderer", "UnitTestingProject")]
public class CounterUnitTests : IDisposable, IClassFixture<CounterService>
{
    public readonly CounterService _counterService;
    private readonly ITestOutputHelper _output;

    public CounterUnitTests(CounterService counterService, ITestOutputHelper output)
    {
        _counterService = counterService;
        _output = output;
    }

    [Fact, Priority(1)]
    public void Add_ShouldIncrementCounter()
    {
        // Arrange
        var expected = 1;

        // Act
        var actual = _counterService.Add();

        _output.WriteLine($"Counter is {actual}");

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact, Priority(2)] //Context from Add_ShouldIncrementCounter does affect this test, since we are sharing the context.
    public void Second_Add_ShouldIncrementCounter()
    {
        // Arrange
        var expected = _counterService.Get();

        // Act
        var actual = _counterService.Add();

        _output.WriteLine($"Second Counter is {actual}");

        // Assert
        Assert.Equal(++expected, actual);
    }

    public void Dispose()
    {
        _counterService.Equals(null);
        GC.SuppressFinalize(this);
    }
}