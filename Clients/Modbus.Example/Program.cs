try
{
    using ModbusRtuClient client = new()
    {
        BaudRate = 19200,
        Parity = Parity.None,
        StopBits = StopBits.One
    };
    client.Connect("COM3");
    client.GetDemo();

    Console.WriteLine("Tests finished. Press any key to continue.");
    Console.ReadLine();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}