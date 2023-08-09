namespace UnitTestingProject;
// dotnet test --filter FullyQualifiedName~UnitTestingProject.MathUnitTests

public class MathUnitTests
{
    private readonly MathService _math;
    public MathUnitTests()
    {
        //Arrange
        _math = new MathService();
    }

    [Fact]
    public void Substract_Numbers()
    {
        //Act
        var result = _math.Substract(4, 2);

        //Assert
        Assert.Equal(2, result);
    }

    [Fact]
    public void Multiply_Numbers()
    {
        //Act
        var result = _math.Multiply(50, 2);

        //Assert
        Assert.Equal(100, result);
    }
}