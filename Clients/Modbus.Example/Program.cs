try
{
    using ModbusRtuClient client = new()
    {
        BaudRate = 19200,
        Parity = Parity.None,
        StopBits = StopBits.One
    };

    try
    {
        client.Connect("COM3");
        client.DoClientWork();
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
    finally
    {
        client.Close();
    }

    Console.WriteLine("Tests finished. Press any key to continue.");
    Console.ReadLine();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}