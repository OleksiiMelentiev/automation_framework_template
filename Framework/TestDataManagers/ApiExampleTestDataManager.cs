using Framework.Models.Api;

namespace Framework.TestDataManagers;

public class ApiExampleTestDataManager : TestDataManagerBase
{
    public ExampleModel GetExampleModel()
    {
        return new ExampleModel()
        {
            Name = GetRandomString(),
        };
    }
}