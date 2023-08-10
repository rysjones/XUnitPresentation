namespace UnitTestingProject.TestFixtures;

public class CacheFixture : IDisposable
{
    public CacheService CacheService { get; }

    public CacheFixture()
    {
        CacheService = new CacheService();
    }

    public void Dispose()
    {
        CacheService.Equals(null);
        GC.SuppressFinalize(this);
    }
}