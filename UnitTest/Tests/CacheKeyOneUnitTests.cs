using UnitTestingProject.TestFixtures;

namespace UnitTestingProject;
// dotnet test --filter "Category=Cache" --logger "console;verbosity=detailed"

[Collection("CacheCollection")]
[Trait("Category", "Cache")]
public class CacheKeyOneUnitTests
{
    private readonly CacheService _cacheService;

    public CacheKeyOneUnitTests(CacheFixture context)
    {
        _cacheService = context.CacheService;
    }

    [Fact]
    public void Get_ReturnsValue_WhenKeyExists()
    {
        // Arrange
        int key = 1;
        string expectedValue = "One";

        // Act
        string actualValue = _cacheService.Get(key);

        // Assert
        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void Exists_ReturnsTrue_WhenKeyExists()
    {
        // Arrange
        int key = 1;

        // Act
        bool exists = _cacheService.Exists(key);

        // Assert
        Assert.True(exists);
    }
}