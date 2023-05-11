namespace WebSoap.Server.Services;

[ServiceContract(ConfigurationName = "twst-a", Namespace = "123456")]
public interface ISoapService
{
    [OperationContract(AsyncPattern = true, Name = "invokeSrv", ReplyAction = "*")] Task<string> InvokeAsync(string text);
}