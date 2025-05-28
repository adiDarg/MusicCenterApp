namespace WebApiClient
{
    public interface IWebClient<T>
    {
        Task<T> GetAsync();
        Task<bool> PostAsync(T model);
        Task<bool> PostAsync(T model, Stream file);
        Task<bool> PostAsync(T model, List<Stream> files);
    }
}
