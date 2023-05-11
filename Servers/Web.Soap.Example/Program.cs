var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestrel(item => item.ListenAnyIP(18080));
builder.Services.AddSoapCore();
builder.Services.AddSingleton<ISoapService, SoapService>();

var app = builder.Build();
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.UseSoapEndpoint<ISoapService>("/service.asmx", new SoapEncoderOptions
{
    WriteEncoding = Encoding.UTF8,
    ReaderQuotas = new XmlDictionaryReaderQuotas
    {
        MaxStringContentLength = int.MaxValue
    }
}, SoapSerializer.DataContractSerializer));
await app.RunAsync();
