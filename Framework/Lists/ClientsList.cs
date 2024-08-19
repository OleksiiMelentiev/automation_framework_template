using Framework.Clients;

namespace Framework.Lists;

public class ClientsList
{
    private ExampleClient? _example;
    public ExampleClient Example => _example ??= new ExampleClient();
}