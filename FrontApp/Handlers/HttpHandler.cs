#nullable enable
using System.Net;

namespace FrontApp.Handlers;

public class HttpHandler
{
    private HttpClient _client;
    private string _port;
    public HttpHandler()
    {
        _port = "5009";
        _client = new();

        GlobalProperties.ApiUri = $"http://localhost:{_port}";
    }
    
    public async Task<HttpResponseMessage> HandlePost(HttpRequestMessage message)
    {
        HttpResponseMessage response;
        Console.WriteLine(message.RequestUri);
        try
        {
             response = await _client.PostAsync(message.RequestUri, null);
        }
        catch (Exception ex)
        {
            response = new HttpResponseMessage() { StatusCode = HttpStatusCode.InternalServerError };
            Console.WriteLine(response.StatusCode.ToString());
        }
        
        return response; 
        
    }

    public async Task<HttpStatusCode> HandlePut(HttpRequestMessage message)
    {
        HttpResponseMessage response = await _client.PutAsync(message.RequestUri, message.Content);

        return response.StatusCode;
    }
    
    public async Task<HttpResponseMessage?> HandleGet(HttpRequestMessage message)
    {
        HttpResponseMessage response;
        try
        {
             response = await _client.GetAsync(message.RequestUri);

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return response.StatusCode == HttpStatusCode.OK ? response : null;
    }
    
    public async Task<HttpResponseMessage?> HandleDelete(HttpRequestMessage message)
    {
        HttpResponseMessage response = await _client.DeleteAsync(message.RequestUri);

        return response.StatusCode == HttpStatusCode.OK ? response : null;
    }
}