namespace Demo.Calculate.Statistics.Boundaries;
public interface IMonoid<TSelf> where TSelf : IMonoid<TSelf>
{
    public static abstract TSelf operator +(TSelf a, TSelf b);
    public static abstract TSelf Zero { get; }
}
public readonly struct MyInt : IMonoid<MyInt>
{
    readonly int _value;
    public MyInt(int value) => _value = value;
    public static MyInt operator +(MyInt a, MyInt b)
    {
        MyInt hh = new(a._value + b._value);

        return new(a._value * b._value);
    }
    public static MyInt Zero => new(3);
}