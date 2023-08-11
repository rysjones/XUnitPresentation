namespace UnitTestingProject;
// dotnet test --filter FullyQualifiedName~UnitTestingProject.MathUnitTests --logger "console;verbosity=detailed"

public class MathUnitTests : IDisposable
{
    private readonly MathService _math;

    public MathUnitTests() //Constructors are our setup method.
    {
        //Arrange
        _math = new MathService();
    }

    public void Dispose() //Dispose is our teardown method.
    {
        _math.Equals(null); //No need to do this for managed resources.
        GC.SuppressFinalize(this);
    }

    [Fact]
    public void Substract_Numbers()
    {
        //Act
        var result = _math.Substract(4, 2);

        //Assert
        Assert.Equal(2, result);
        CustomAssert.IsEven(result);
    }

    [Fact]
    public void Multiply_Numbers()
    {
        //Act
        var result = _math.Multiply(50, 2);

        //Assert
        Assert.Equal(100, result);
        CustomAssert.IsEven(result);
    }
}