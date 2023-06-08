// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
//https://www.bilibili.com/read/cv16999489/

try
{
    long level = 3;
    string filename = "focas.log";
    //
    Focas1.cnc_startupprocess(level, filename);


    var gg = Focas1.cnc_allclibhndl3("192.168.26.126", 8193, 5, out Focas1._handle);

    //退出
    Focas1.cnc_exitprocess();
}
catch(Exception e)
{
    Console.WriteLine(e.ToString());
}
