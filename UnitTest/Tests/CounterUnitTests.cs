namespace UnitTestingProject;
// dotnet test --filter FullyQualifiedName~UnitTestingProject.CounterUnitTests

public class CounterUnitTests : IDisposable
{
    private readonly CounterService _counterService;

    public CounterUnitTests() //Constructors are our setup method.
    {
        _counterService = new CounterService();
    }

    [Fact]
    public void Add_ShouldIncrementCounter()
    {
        // Arrange
        var expected = 1;

        // Act
        var actual = _counterService.Add();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact] //Context from Add_ShouldIncrementCounter doesnt affect this test.
    public void Same_Add_ShouldIncrementCounter()
    {
        // Arrange
        var expected = 1;

        // Act
        var actual = _counterService.Add();

        // Assert
        Assert.Equal(expected, actual);
    }

    public void Dispose() //Dispose is our teardown method (if necessary) for managed code, it is not.
    {
        _counterService.Equals(null);
    }
}