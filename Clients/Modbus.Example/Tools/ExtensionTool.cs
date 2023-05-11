namespace ModbusDemo.Client.Tools;
internal static class ExtensionTool
{
    internal static void GetDemo(this ModbusRtuClient client)
    {
        var slaveId = 0x01; //從站編號
        var startingAddress = 0x1233; //起始位置
        var count = 2; //暫存器數量

        //04h Read Input Registers
        var datas = client.ReadInputRegisters<ushort>(slaveId, startingAddress, count);
    }
}