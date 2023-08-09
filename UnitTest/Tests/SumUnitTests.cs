namespace UnitTestingProject;
// dotnet test --filter FullyQualifiedName=UnitTestingProject.SumUnitTests.Add_Number

//No need for class decoration
public class SumUnitTests
{
    [Fact] //Fact: Tests which are always true.
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