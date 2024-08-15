using Framework.Helpers;
using Microsoft.Playwright;
using NUnit.Framework;

namespace Framework.Abstractions;

public class UiTestBase : TestBase
{
    protected IPage Page { get; private set; } = default!;
    
    [SetUp]
    public async Task Setup()
    {
        Page = await PlaywrightHelper.GetPageAsync();
        await PlaywrightHelper.StartTracingAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        await PlaywrightHelper.StopTracing();
        await PlaywrightHelper.CloseAsync();
    }
}