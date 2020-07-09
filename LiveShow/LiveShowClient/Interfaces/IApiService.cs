using System.Threading.Tasks;

namespace LiveShowClient.Interfaces
{
    public interface IApiService
    {
        Task<T> GetContentFromHttpAsync<T>(string url);
        Task<string> PostDataAsync<T>(string uri, T data);
        Task<string> PatchDataAsync<T>(string uri, T data);
        Task<string> DeleteDataAsync(string uri);
        Task<string> PutAsync<T>(string uri, T data);
    }
}
