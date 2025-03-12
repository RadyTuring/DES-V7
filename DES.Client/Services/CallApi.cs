using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Services;

public class CallApi: ICallApi
{
     
    private HttpClient client;
    private readonly string baseAddress;
   
    public static int Progress { get; private set; }
    public CallApi(IConfiguration configuration, HttpClient _client)
    {
        baseAddress = configuration.GetSection("api")["baseurl"];
        client = _client;
        client.BaseAddress = new Uri(baseAddress);
    }
    public async Task<TResponse> PostDt<TRequest, TResponse>(string url, TRequest data)
    {
        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await client.PostAsync(url, content);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var jsonData = JsonConvert.DeserializeObject<TResponse>(jsonResponse);
            return jsonData;
        }
        else
        {
            throw new Exception("Error: Unable to complete the request");
        }
    }

    public async Task<T> GetAsync<T>(string apiName, object model)
    {
        // Serialize the model to JSON and send it in the request body
        var response = await client.PostAsJsonAsync(apiName, model);

        // Ensure the request was successful
        response.EnsureSuccessStatusCode();

        // Read the response content and deserialize it to the expected type
        var data = await response.Content.ReadAsAsync<T>();

        return data;
    }

    public IEnumerable<T> Get<T>(string apiName)
    {
        var res = client.GetAsync(apiName).Result;
        var data = res.Content.ReadAsAsync<IEnumerable<T>>().Result;
        return data;
    }
    public T GetById<T>(string apiName)
    {
        var res = client.GetAsync(apiName).Result;
        var data = res.Content.ReadAsAsync<T>().Result;
        return data;
    }
    public bool Create(string apiName, object o)
    {
        var postTask = client.PostAsJsonAsync(apiName, o);
        postTask.Wait();
        var result = postTask.Result;
        return result.IsSuccessStatusCode;
    }
    public bool Delete(string apiName)
    {
        var postTask = client.DeleteAsync(apiName);
        postTask.Wait();
        var result = postTask.Result;
        return result.IsSuccessStatusCode;
    }

    public bool Update(string apiName, object o)
    {
        var postTask = client.PutAsJsonAsync(apiName, o);
        postTask.Wait();
        var result = postTask.Result;
        return result.IsSuccessStatusCode;
    }
}
