namespace Modbus.Example.Tools;
internal static class FluentModbusTool
{
    internal static void GetDemo()
    {
        var slaveId = 0x01; //從站編號
        var startingAddress = 0x123E; //起始位置 0x1233
        var count = 2; //暫存器數量

        //04h Read Input Registers
        using ModbusRtuClient client = new() { BaudRate = 19200, Parity = Parity.None, StopBits = StopBits.One };
        {
            client.Connect("COM3");
            var datas = client.ReadInputRegisters<uint>(slaveId, startingAddress, count);
        }
    }
}