namespace ModbusDemo.Client.Tools;
internal static class ExtensionTool
{
    internal static void DoClientWork(this ModbusRtuClient client)
    {
        var unitIdentifier = 2;
        var startingAddress = 0x1300;


        // ReadHoldingRegisters = 0x03,        // FC03
        Span<uint> datas = client.ReadHoldingRegisters<uint>(unitIdentifier, startingAddress, 1);
        Console.WriteLine(datas[default]);
    }
}