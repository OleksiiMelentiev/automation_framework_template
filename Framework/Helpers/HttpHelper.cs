using System.Net.Http.Json;

namespace Framework.Helpers;

public static class HttpHelper
{
    public static async Task<T> GetModelFromResponseAsync<T>(HttpResponseMessage response)
    {
        var responseModel = await response.Content.ReadFromJsonAsync<T>();
        return responseModel == null
            ? throw new ArgumentException("Can't get correct answer from api")
            : responseModel;
    }
}