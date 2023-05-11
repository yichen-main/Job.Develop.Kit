
//https://www.cnblogs.com/wucy/p/15128365.html
//https://www.cnblogs.com/shanfeng1000/p/13402152.html

//設置3000毫秒(即3秒)後取消
CancellationTokenSource cancellationTokenSourceA = new(3000);
CancellationToken cancellationTokenA = cancellationTokenSourceA.Token;
cancellationTokenA.Register(() => Console.WriteLine("我被取消了."));

Console.WriteLine("先等五秒鐘.");
await Task.Delay(5000);

Console.WriteLine("手動取消.");
cancellationTokenSourceA.Cancel();

//
CancellationTokenSource tokenSource = new();
CancellationToken cancellationToken = tokenSource.Token;

//新建任务
Task t = Task.Run(() =>
{
    while (true)
    {
        //检测任务是否已经被取消
        if (tokenSource.IsCancellationRequested)
        {
            Console.WriteLine("Task canceled");
            break;
        }

        //任务开始
        Console.WriteLine("Task start!");

        //模拟耗时的操作
        Thread.Sleep(1000);

        //任务结束
        Console.WriteLine("Task finished!");
    }
}, cancellationToken);

Console.WriteLine();
Thread.Sleep(10);

//用户控制是否取消任务
while (true)
{
    Console.Write("請切換到英文輸入法");
    Console.WriteLine("取消任務請按Y");
    if ((Console.ReadKey()).Key == ConsoleKey.Y)
    {
        //任务取消
        tokenSource.Cancel();
    }
}