try
{
    var a1 = ExtensionTool.Calc(4, 5);
    var a2 = ExtensionTool.Calc(3.2, 5);

    var JJ1 = new MyInt(3) + new MyInt(4) + new MyInt(4);
    var JJ2 = new MyInt(4);
    var JJ3 = new MyInt(5);
    var sum = ExtensionTool.AddAll(JJ1, new MyInt(4), new MyInt(5));
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}