using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

namespace Core.Security.MicrosoftAuth;

public class MicrosoftAuthAdapter : IMicrosoftAuthAdapter
{
    private readonly MicrosoftAuthOptions _options;
    private readonly HttpClient _httpClient;

    public MicrosoftAuthAdapter(IConfiguration configuration)
    {
        _options = configuration.GetSection("MicrosoftAuthAdapter").Get<MicrosoftAuthOptions>();
        _httpClient = new HttpClient();
    }

    public async Task<MicrosoftUserDetail> GetMicrosoftUserDetail(string microsoftAccessToken)
    {
        MicrosoftUserDetail microsoftUserDetail;

        string url = $"{_options.GraphEndPoint}/v1.0/me";
        using (HttpRequestMessage request = new(HttpMethod.Get, url))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", microsoftAccessToken);
            HttpResponseMessage response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            microsoftUserDetail = await response.Content.ReadFromJsonAsync<MicrosoftUserDetail>();
        }

        return microsoftUserDetail;
    }
}