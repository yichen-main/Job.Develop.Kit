namespace Hardware.Diagnosis.Events;

[EventSource(Name = "Example.MyEventCounterSource")]
public sealed class MyEventCounterSource : EventSource
{
    public static readonly MyEventCounterSource Instance = new();
    readonly IncrementingEventCounter _myCounter;
    MyEventCounterSource() => _myCounter = new IncrementingEventCounter("my-counter", this)
    {
        DisplayName = "My Incrementing Counter"
    };
    public void Up() => _myCounter.Increment();
    protected override void Dispose(bool disposing)
    {
        _myCounter.Dispose();
        base.Dispose(disposing);
    }
}