namespace UnitTestingProject;

// dotnet test --filter FullyQualifiedName=UnitTestingProject.SumUnitTests.Add_Number

public class SumUnitTests
{
    [Fact]
    public void Add_Numbers()
    {
        //Arrange
        var math = new MathService();

        //Act
        var result = math.Add(1, 2);

        //Assert
        Assert.Equal(3, result);
    }
}

