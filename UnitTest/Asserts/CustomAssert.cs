using Xunit.Sdk;


public class CustomAssert : Assert
{
    public static void IsEven( int value)
    {
        if (value % 2 != 0)
            throw IsEvenException.ForNonEvenValue(value);
    }
}

public class IsEvenException : XunitException
{
    IsEvenException(string message)
        : base(message)
    { }

    public static IsEvenException ForNonEvenValue(int value) =>
        new($"The value {value} was not even.");
}