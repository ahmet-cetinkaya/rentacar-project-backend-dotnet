using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

namespace Core.Security.MicrosoftAuth;

public class MicrosoftAuth : IMicrosoftAuth
{
    private MicrosoftAuthOptions _options;
    private readonly HttpClient _httpClient;

    public MicrosoftAuth(IConfiguration configuration)
    {
        _options = configuration.GetSection("MicrosoftAuth").Get<MicrosoftAuthOptions>();
        _httpClient = new HttpClient();
    }

    public async Task<MicrosoftUserDetail> getMicrosoftUserDetail(string microsoftAccessToken)
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