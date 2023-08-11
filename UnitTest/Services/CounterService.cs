namespace UnitTestingProject.Services;

public class CounterService
{
    private int _counter;

    public CounterService() => _counter = 0;

    public int Add() => ++_counter;

    public int Get() => _counter;
}