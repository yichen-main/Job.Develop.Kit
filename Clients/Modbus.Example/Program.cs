try
{

     FluentModbusTool.GetDemo();

    Console.WriteLine("Tests finished. Press any key to continue.");
    Console.ReadLine();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}