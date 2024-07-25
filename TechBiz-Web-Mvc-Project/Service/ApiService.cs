using AspnetCoreMvcFull.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace AspnetCoreMvcFull.Service
{
  public class ApiService
  {
    private readonly HttpClient m_HttpClient;
    private string m_token;

    public ApiService()
    {
      m_HttpClient = new HttpClient();
      m_HttpClient.BaseAddress = new Uri("https://localhost:7035");
      m_HttpClient.DefaultRequestHeaders.Accept.Clear();
      m_HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public void SetToken(string token)
    {
      m_token = token;
      if (!string.IsNullOrEmpty(m_token))
      {
        m_HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", m_token);
      }
    }
    public async Task<ResponseApiModel> GetDataAsync(string endpoint)
    {
      if (!string.IsNullOrEmpty(m_token))
      {
        m_HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", m_token);
      }

      HttpResponseMessage response = await m_HttpClient.GetAsync(endpoint);
      response.EnsureSuccessStatusCode();
      string responseBody = await response.Content.ReadAsStringAsync();


      return JsonConvert.DeserializeObject<ResponseApiModel>(responseBody);
    }

  }
}
