try
{
    using SerialPort port = new("COM3");
    port.BaudRate = 19200;
    port.DataBits = 8;
    port.Parity = Parity.None;
    port.StopBits = StopBits.One;
    port.Open();

    // create modbus master
    IModbusSerialMaster master = ModbusSerialMaster.CreateAscii(port);

    // 讀取輸入寄存器 (Fun04)。
    // 功率計將返回 36 個功率參數（電壓、電流、功率、能量...等）
    ushort startAddress = 0x1100;  // 4352;
    ushort numRegister = 72; // 36*2                   

    ushort[] PowerParameters = new ushort[72];
    // 設置函數參數以獲取功率計中的所有輸入寄存器值。
    PowerParameters = master.ReadInputRegisters(1, startAddress, numRegister);

    // 將兩個 UInt16 值轉換為 IEEE 32 浮點格式值。
    float[] fvalue = new float[36];
    for (int i = 0; i < (numRegister / 2); i++)
    {
        fvalue[i] = ModbusUtility.GetSingle(PowerParameters[2 * i + 1], PowerParameters[2 * i]);
    }

    //Output the result.
    string titleformate = "{0,13}   {1,13}   {2,13}   {3,13}   {4,13}";
    string dataformate = "{0,13}   {1,13:f3}   {2,13:f3}   {3,13:f3}   {4,13:f3}";
    Console.WriteLine(string.Format(titleformate, "", "<<Channel 1>>", "<<Channel 2>>", "<<Channel 3>>", "<<Channel 4>>"));
    Console.WriteLine(string.Format(dataformate, "[Voltage]", fvalue[0], fvalue[9], fvalue[18], fvalue[27]));
    Console.WriteLine(string.Format(dataformate, "[Current]", fvalue[1], fvalue[10], fvalue[19], fvalue[28]));
    Console.WriteLine(string.Format(dataformate, "[kW]", fvalue[2], fvalue[11], fvalue[20], fvalue[29]));
    Console.WriteLine(string.Format(dataformate, "[kVar]", fvalue[3], fvalue[12], fvalue[21], fvalue[30]));
    Console.WriteLine(string.Format(dataformate, "[kVA]", fvalue[4], fvalue[13], fvalue[22], fvalue[31]));
    Console.WriteLine(string.Format(dataformate, "[PF]", fvalue[5], fvalue[14], fvalue[23], fvalue[32]));
    Console.WriteLine(string.Format(dataformate, "[kWh]", fvalue[6], fvalue[15], fvalue[24], fvalue[33]));
    Console.WriteLine(string.Format(dataformate, "[kvarh]", fvalue[7], fvalue[16], fvalue[25], fvalue[34]));
    Console.WriteLine(string.Format(dataformate, "[kVah]", fvalue[8], fvalue[17], fvalue[26], fvalue[35]));

}
catch(Exception e)
{

}