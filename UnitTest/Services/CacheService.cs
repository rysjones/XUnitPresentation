using System.Collections.Concurrent;

namespace UnitTestingProject.Services;

public class CacheService
{
    private readonly ConcurrentDictionary<int, string> keyValuePairs = new();

    public CacheService()
    {
        keyValuePairs.TryAdd(1, "One");
        keyValuePairs.TryAdd(2, "Two");
        keyValuePairs.TryAdd(3, "Three");
    }

    public string Get(int key) => keyValuePairs[key];

    public void Add(int key, string value) => keyValuePairs.TryAdd(key, value);

    public void Remove(int key) => keyValuePairs.TryRemove(key, out _);

    public void Update(int key, string value) => keyValuePairs[key] = value;

    public bool Exists(int key) => keyValuePairs.ContainsKey(key);

    public int Count() => keyValuePairs.Count;
}