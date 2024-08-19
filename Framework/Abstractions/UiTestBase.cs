using Framework.Helpers;
using Framework.Lists;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Framework.Abstractions;

public class UiTestBase : TestBase
{
    protected readonly PagesList Pages = new();


    [SetUp]
    public async Task Setup()
    {
        await PlaywrightHelper.CreateAsync();
        await PlaywrightHelper.StartTracingAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        var tracingPath = await PlaywrightHelper.StopTracing();

        var isPassed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
        if (isPassed == false)
        {
            var screenPath = await PlaywrightHelper.TakeScreenshotAsync();
            ExtentReports.LogScreenshot(screenPath);
            ExtentReports.LogScreenshot(tracingPath, "tracing (open in a new tab to download)");
        }

        await PlaywrightHelper.CloseAsync();
    }
}