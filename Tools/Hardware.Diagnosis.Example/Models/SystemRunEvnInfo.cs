namespace Hardware.Diagnosis.Models;

[Display(Name = "運行環境")]
public sealed class SystemRunEvnInfo
{
    [Display(Name = "機器名稱")] public static string MachineName => Environment.MachineName;
    [Display(Name = "用戶網絡域名")] public static string UserDomainName => Environment.UserDomainName;
    [Display(Name = "分區磁盤")] public static string GetLogicalDrives => string.Join(", ", Environment.GetLogicalDrives());
    [Display(Name = "系統目錄")] public static string SystemDirectory => Environment.SystemDirectory;
    [Display(Name = "系統已運行時間(毫秒)")] public static int TickCount => Environment.TickCount;
    [Display(Name = "是否在交互模式中運行")] public static bool UserInteractive => Environment.UserInteractive;
    [Display(Name = "當前關聯用戶名")] public static string UserName => Environment.UserName;
    [Display(Name = "Web程序核心框架版本")] public static string Version => Environment.Version.ToString();

    //對Linux無效
    [Display(Name = "磁盤分區")] public static string SystemDrive => Environment.ExpandEnvironmentVariables("%SystemDrive%");

    //對Linux無效
    [Display(Name = "系統目錄")] public static string SystemRoot => Environment.ExpandEnvironmentVariables("%SystemRoot%");
}