namespace WebSoap.Server.Services;
public sealed class SoapService : ISoapService
{//https://www.gushiciku.cn/pl/gwGT/zh-tw
    public Task<string> InvokeAsync(string text)
    {
        return Task.FromResult(text);
    }
}