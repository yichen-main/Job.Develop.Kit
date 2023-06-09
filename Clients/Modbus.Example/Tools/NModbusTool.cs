﻿namespace Modbus.Example.Tools;
internal class NModbusTool
{
    internal void GetDemo()
    {
        using (SerialPort port = new("COM3"))
        {
            //// configure serial port
            //port.BaudRate = 9600;
            //port.DataBits = 8;
            //port.Parity = Parity.None;
            //port.StopBits = StopBits.One;
            //port.Open();

            //var factory = new ModbusFactory();
            //IModbusMaster master = factory.CreateRtuMaster((NModbus.IO.IStreamResource)port);

            //byte slaveId = 1;
            //ushort startAddress = 100;
            //ushort[] registers = new ushort[] { 1, 2, 3 };

            //// write three registers
            //master.WriteMultipleRegisters(slaveId, startAddress, registers);
        }


        //using (SerialPort port = new SerialPort("COM3"))
        //{
        //    port.BaudRate = 19200;
        //    port.DataBits = 8;
        //    port.Parity = Parity.None;
        //    port.StopBits = StopBits.One;

        //    var factory = new ModbusFactory();
        //    IModbusSerialMaster master = factory.CreateRtuMaster((IStreamResource)port);

        //    //IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(port);

        //    byte slaveId = 1;
        //    ushort startAddress = 0x1240;
        //    ushort numRegisters = 2;

        //    ushort[] registers = master.ReadHoldingRegisters(slaveId, startAddress, numRegisters);

        //    for (int i = 0; i < numRegisters; i++)
        //    {
        //        Console.WriteLine($"Register {i + startAddress}={registers[i]}");
        //    }
        //}
    }

    /// <summary>
    /// 串口参数
    /// </summary>
    public void SetSerialPort()
    {
        //获取当前计算机所有的串行端口名
        string[] serialProtArray = SerialPort.GetPortNames();

        //端口名，如COM1
        serialPort.PortName = "COM1";

        //波特率
        serialPort.BaudRate = 9600;

        //奇偶校验
        serialPort.Parity = Parity.None;

        //数据位
        serialPort.DataBits = 8;

        //停止位
        serialPort.StopBits = StopBits.One;

        //串口接收数据事件
        serialPort.DataReceived += ReceiveDataMethod;
    }

    /// <summary>
    /// 打开串口
    /// </summary>
    public void Open()
    {
        //打开串口
        serialPort.Open();
    }

    /// <summary>
    /// 关闭串口
    /// </summary>
    public void Close()
    {
        serialPort.Close();
    }

    /// <summary>
    /// 发送数据
    /// </summary>
    /// <param name="data">要发送的数据</param>
    public void SendDataMethod(byte[] data)
    {
        //获取串口状态，true为已打开，false为未打开
        bool isOpen = serialPort.IsOpen;

        if (!isOpen)
        {
            Open();
        }

        //发送字节数组
        //参数1：包含要写入端口的数据的字节数组。
        //参数2：参数中从零开始的字节偏移量，从此处开始将字节复制到端口。
        //参数3：要写入的字节数。 
        serialPort.Write(data, 0, data.Length);
    }

    /// <summary>
    /// 发送数据
    /// </summary>
    /// <param name="data">要发送的数据</param>
    public void SendDataMethod(string data)
    {
        //获取串口状态，true为已打开，false为未打开
        bool isOpen = serialPort.IsOpen;

        if (!isOpen)
        {
            Open();
        }

        //直接发送字符串
        serialPort.Write(data);
    }

    /// <summary>
    /// 串口接收到数据触发此方法进行数据读取
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ReceiveDataMethod(object sender, SerialDataReceivedEventArgs e)
    {
        //读取串口缓冲区的字节数据
        byte[] result = new byte[serialPort.BytesToRead];
        serialPort.Read(result, 0, serialPort.BytesToRead);
    }

    //串口实例
    SerialPort serialPort = new SerialPort();

}