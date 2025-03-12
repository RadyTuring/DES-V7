namespace Services;

public interface ICallApi
{
    IEnumerable<T> Get<T>(string apiName);
    public   Task<TResponse> PostDt<TRequest, TResponse>(string url, TRequest data);
    T GetById<T>(string apiName);
        Task<T> GetAsync<T>(string apiName, object model);

     bool Create(string apiName, object o);

    bool Delete(string apiName);

    bool Update(string apiName, object o);
    
}
