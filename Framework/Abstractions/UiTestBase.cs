using Framework.Helpers;
using Framework.Lists;
using NUnit.Framework;

namespace Framework.Abstractions;

public class UiTestBase : TestBase
{
    protected PagesList Pages = new();


    [SetUp]
    public async Task Setup()
    {
        await PlaywrightHelper.CreateAsync();
        await PlaywrightHelper.StartTracingAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        await PlaywrightHelper.StopTracing();
        await PlaywrightHelper.CloseAsync();
    }
}