try
{
    var path = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? string.Empty;

    //硬碟空間剩餘 百分比
    var diskUsage = CheckDiskSpace(path);

    //cpu 使用量
    var cpuUsage = await GetCpuUsageForProcess();

    //cpu 使用量轉換百分比
    var cpuPercentage = cpuUsage.ToString("0.##%");
    {
        var a = GetApplicationRunInfo();
        var b = GetSystemPlatformInfo();
        var c = GetSystemRunEvnInfo();
        var d = GetEnvironmentVariables();
        ConsoleInfo(a.Item1, a.Item2);
        ConsoleInfo(b.Item1, b.Item2);
        ConsoleInfo(c.Item1, c.Item2);
        ConsoleInfo(d.Item1, d.Item2);
    }
    Console.Read();
}
catch (Exception e)
{

}