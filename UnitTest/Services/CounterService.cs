namespace UnitTestingProject.Services;

internal class CounterService
{
    private int _counter;

    public CounterService()
    {
        _counter = 0;
    }

    public int Add()
    {
        return ++_counter;
    }

    public int Get()
    {
        return _counter;
    }
}