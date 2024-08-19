using Framework.Models.Api;
using Newtonsoft.Json;

namespace Framework.Clients;

public class ExampleClient : ClientBase
{
    protected override string Url => BaseUrl + "/todo";

    private readonly IList<ExampleModel> _models = new List<ExampleModel>();

    public async Task<HttpResponseMessage> GetAsync(Guid id)
    {
        // todo: implement
        var model = _models.FirstOrDefault(m => m.Id == id);
        return new HttpResponseMessage()
        {
            Content = new StringContent(JsonConvert.SerializeObject(model)),
        };
    }

    public async Task<HttpResponseMessage> PostAsync(ExampleModel exampleModel)
    {
        // todo: implement
        _models.Add(exampleModel);
        return new HttpResponseMessage()
        {
            Content = new StringContent(JsonConvert.SerializeObject(exampleModel)),
        };
    }
}