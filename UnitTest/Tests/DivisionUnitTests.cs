namespace UnitTestingProject;
// dotnet test --filter FullyQualifiedName~UnitTestingProject.DivisionUnitTests --logger "console;verbosity=detailed"

public class DivisionUnitTests
{
    [Theory] //Theory: Tests which are only true for a particular set of data.
    [InlineData(4, 2, 2)]
    [InlineData(16, 2, 8)]
    [InlineData(1004, 2, 502)]
    public void Divide_Numbers(int dividend, int divisor, int quotient)
    {
        //Arrange
        var math = new MathService();

        //Act
        var result = math.Divide(dividend, divisor);

        //Assert
        Assert.Equal(quotient, result);
    }

    [Fact] //Fact: Tests which are always true.
    public void Divide_Numbers_By_Zero()
    {
        //Arrange
        var math = new MathService();

        //Act
        var result = Assert.Throws<DivideByZeroException>(() => math.Divide(1, 0));

        //Assert
        Assert.Equal("Attempted to divide by zero.", result.Message);
    }
}