using System.Management.Automation;

PowerShellHelper.Execute("([System.Management.Automation.ActionPreference], [System.Management.Automation.AliasAttribute]).FullName");


static class PowerShellHelper
{
    public static void Execute(string command)
    {
        using var ps = PowerShell.Create();
        var results = ps.AddScript(command).Invoke();
        foreach (var result in results)
        {
            Console.WriteLine(result);
        }
    }
}