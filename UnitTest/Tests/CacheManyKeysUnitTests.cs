using UnitTestingProject.TestFixtures;

namespace UnitTestingProject;
// dotnet test --filter "Category=Cache" --logger "console;verbosity=detailed"

[Collection("CacheCollection")]
[Trait("Category", "Cache")]
public class CacheManyKeysUnitTests
{
    private readonly CacheService _cacheService;

    public CacheManyKeysUnitTests(CacheFixture context)
    {
        _cacheService = context.CacheService;
    }

    [Fact]
    public void Count_ReturnsNumberOfKeyValuePairs()
    {
        // Arrange
        int expectedCount = 3;

        // Act
        int actualCount = _cacheService.Count();

        // Assert
        Assert.True(actualCount >= expectedCount);
    }

    [Fact]
    public void Add_AddsKeyValuePair_WhenKeyDoesNotExist()
    {
        // Arrange
        int key = 4;
        string value = "Four";

        // Act
        _cacheService.Add(key, value);

        // Assert
        Assert.Equal(value, _cacheService.Get(key));
    }

    [Fact]
    public void Update_UpdatesKeyValuePair_WhenKeyExists()
    {
        // Arrange
        int key = 1;
        string value = "New One";

        // Act
        _cacheService.Update(key, value);

        // Assert
        Assert.Equal(value, _cacheService.Get(key));
    }
}