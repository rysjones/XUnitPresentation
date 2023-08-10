namespace UnitTestingProject;
// dotnet test --filter FullyQualifiedName~UnitTestingProject.MathUnitTests

public class MathUnitTests : IClassFixture<MathService>
{
    private readonly MathService _math;
    public MathUnitTests(MathService math) //Constructors are our setup method.
    {
        //Arrange
        _math = math;
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
