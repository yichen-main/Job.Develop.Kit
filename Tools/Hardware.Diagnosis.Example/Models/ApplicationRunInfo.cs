namespace Hardware.Diagnosis.Models;

[Display(Name = "運行信息")]
public sealed class ApplicationRunInfo
{
    readonly double _usedMem;
    readonly double _usedCPUTime;
    public ApplicationRunInfo()
    {
        var proc = Process.GetCurrentProcess();
        var mem = proc.WorkingSet64;
        var cpu = proc.TotalProcessorTime;
        _usedMem = mem / 1024.0;
        _usedCPUTime = cpu.TotalMilliseconds;
    }
    [Display(Name = "進程已使用物理內存(kb)")] public double UsedMem { get { return _usedMem; } }
    [Display(Name = "進程已佔耗CPU時間(ms)")] public double UsedCPUTime { get { return _usedCPUTime; } }
}