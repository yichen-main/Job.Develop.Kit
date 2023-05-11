namespace Hardware.Diagnosis.Features;
public static class EnvironmentInfo
{
    public static int CheckDiskSpace(string driveLetter)
    {
        DriveInfo drive = new(driveLetter);

        //硬碟空間總共 Bytes
        var totalBytes = drive.TotalSize;

        //硬碟空間剩餘 Bytes
        var freeBytes = drive.AvailableFreeSpace;

        var freePercent = (int)(100 * freeBytes / totalBytes);

        return freePercent;
    }
    public static async Task<double> GetCpuUsageForProcess()
    {
        var startTime = DateTime.UtcNow;
        var startCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
        {
            await Task.Delay(500);
            var endCpuUsage = Process.GetCurrentProcess().TotalProcessorTime;
            var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
            var totalMsPassed = (DateTime.UtcNow - startTime).TotalMilliseconds;
            return cpuUsedMs / (Environment.ProcessorCount * totalMsPassed);
        }
    }

    /// <summary>
    /// 獲取程序運行資源信息
    /// </summary>
    /// <returns></returns>
    public static (string, List<KeyValuePair<string, object>>) GetApplicationRunInfo()
    {
        ApplicationRunInfo info = new();
        return GetValues(info);
    }
    /// <summary>
    /// 獲取系統運行平台信息
    /// </summary>
    /// <returns></returns>
    public static (string, List<KeyValuePair<string, object>>) GetSystemPlatformInfo()
    {
        SystemPlatformInfo info = new();
        return GetValues(info);
    }
    /// <summary>
    /// 獲取系統運行環境信息
    /// </summary>
    /// <returns></returns>
    public static (string, List<KeyValuePair<string, object>>) GetSystemRunEvnInfo()
    {
        SystemRunEvnInfo info = new();
        return GetValues(info);
    }

    /// <summary>
    /// 獲取系統全部環境變量
    /// </summary>
    /// <returns></returns>
    public static (string, List<KeyValuePair<string, object>>) GetEnvironmentVariables()
    {
        List<KeyValuePair<string, object>> list = new();
        IDictionary environmentVariables = Environment.GetEnvironmentVariables();
        foreach (DictionaryEntry de in environmentVariables)
        {
            list.Add(new KeyValuePair<string, object>(de.Key.ToString(), de.Value));
        }
        return ("系統環境變量", list);
    }

    public static void ConsoleInfo(string title, List<KeyValuePair<string, object>> list)
    {
        Console.WriteLine("\n***********" + title + "***********");
        foreach (var item in list)
        {
            Console.WriteLine(item.Key + ":" + item.Value);
        }
    }

    /// <summary>
    /// 獲取 [Display] 特性的屬性 Name 的值
    /// </summary>
    /// <param name="attrs"></param>
    /// <returns></returns>
    private static string GetDisplayNameValue(IList<CustomAttributeData> attrs)
    {
        var argument = attrs.FirstOrDefault(x => x.AttributeType.Name == nameof(DisplayAttribute)).NamedArguments;
        return argument.FirstOrDefault(x => x.MemberName == nameof(DisplayAttribute.Name)).TypedValue.Value.ToString();
    }

    /// <summary>
    /// 獲取某個類型的值以及名稱
    /// </summary>
    /// <typeparam name="TInfo"></typeparam>
    /// <param name="info"></param>
    /// <returns></returns>
    private static (string, List<KeyValuePair<string, object>>) GetValues<TInfo>(TInfo info)
    {
        List<KeyValuePair<string, object>> list = new();
        Type type = info.GetType();
        PropertyInfo[] pros = type.GetProperties();
        foreach (var item in pros)
        {
            var name = GetDisplayNameValue(item.GetCustomAttributesData());
            var value = GetPropertyInfoValue(item, info);
            list.Add(new KeyValuePair<string, object>(name, value));
        }
        return (GetDisplayNameValue(info.GetType().GetCustomAttributesData()), list);
    }

    /// <summary>
    /// 獲取屬性的值
    /// </summary>
    /// <param name="info"></param>
    /// <param name="obj">實例</param>
    /// <returns></returns>
    private static object GetPropertyInfoValue(PropertyInfo info, object obj)
    {
        return info.GetValue(obj);
    }

    [Display(Name = "操作系統")]
    public static string OSDescription { get { return RuntimeInformation.OSDescription; } }

    /// <summary>
    /// 操作系统架构（<see cref="Architecture">）
    /// </summary>
    public static string OSArchitecture { get; } = RuntimeInformation.OSArchitecture.ToString();

    /// <summary>
    /// 是否為Windows操作系統
    /// </summary>
    public static bool IsOSPlatform { get; } = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
}