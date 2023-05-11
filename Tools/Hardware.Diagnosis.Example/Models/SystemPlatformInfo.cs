namespace Hardware.Diagnosis.Models;

[Display(Name = "系統運行平台")]
public sealed class SystemPlatformInfo
{
    [Display(Name = "運行框架")] public static string FrameworkDescription => RuntimeInformation.FrameworkDescription;
    [Display(Name = "操作系統")] public static string OSDescription => RuntimeInformation.OSDescription;
    [Display(Name = "操作系統版本")] public static string OSVersion => Environment.OSVersion.ToString();
    [Display(Name = "平台架構")] public static string OSArchitecture => RuntimeInformation.OSArchitecture.ToString();
}