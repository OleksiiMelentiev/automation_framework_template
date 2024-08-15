using Framework.Helpers;
using Microsoft.Playwright;
using NUnit.Framework;

namespace Framework.Abstractions;

public class UiTestBase : TestBase
{
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