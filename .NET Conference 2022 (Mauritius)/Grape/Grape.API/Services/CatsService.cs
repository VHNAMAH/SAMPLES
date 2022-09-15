using System.Text.Json;
using Grape.API.Models;

namespace Grape.API.Services;

public interface ICatsService {
    Task<CatFact> FetchFact();
}

public class CatsService : ICatsService {
    private readonly IHttpClientFactory _httpClientFactory;
    public CatsService(IHttpClientFactory httpClientFactory)
    {
        this._httpClientFactory = httpClientFactory;
    }
    public async Task<CatFact> FetchFact()
    {
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://catfact.ninja/fact");

        var httpClient = _httpClientFactory.CreateClient();
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            using var contentStream =
                await httpResponseMessage.Content.ReadAsStreamAsync();
            
            var result = await JsonSerializer.DeserializeAsync<CatFact>(contentStream);
            return result;
        }

        return null;
    }
}