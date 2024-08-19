namespace Framework.Clients;

public abstract class ClientBase
{
    protected abstract string Url { get; }

    protected const string BaseUrl = "todo";
    protected HttpClient Client { get; private set; } = new();
}