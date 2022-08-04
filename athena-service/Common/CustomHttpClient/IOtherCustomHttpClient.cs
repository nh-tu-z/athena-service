namespace AthenaService.Common.CustomHttpClient
{
    public interface IOtherCustomHttpClient
    {
        Task<T?> GetDataAsync<T>(string url, string? authorization = null);
        Task<string> GetStringAsync(string url);
    }
}
