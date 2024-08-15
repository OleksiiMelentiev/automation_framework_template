using Microsoft.Playwright;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Framework.Helpers;

public static class PlaywrightHelper
{
    private static IPlaywright PlaywrightObj { get; set; } = default!;
    private static IBrowser BrowserObj { get; set; } = default!;
    private static IBrowserContext Context { get; set; } = default!;
    private static IPage? Page { get; set; }
    private static readonly bool IsDebugging = false; // set true to debug


    public static async Task CloseAsync()
    {
        if (Page == null)
        {
            throw new NullReferenceException("The page is not created");
        }

        await Page.CloseAsync();
        await Context.CloseAsync();
        await BrowserObj.CloseAsync();
        PlaywrightObj.Dispose();
    }

    public static async Task CreateAsync()
    {
        if (Page != null)
        {
            return;
        }

        PlaywrightObj = await Playwright.CreateAsync();
        BrowserObj = await CreateBrowserAsync();
        Page = await CreatePageAsync();
    }

    public static IPage GetPage()
    {
        return Page ?? throw new ArgumentException("Playwright instance is not created");
    }

    public static async Task StartTracingAsync()
    {
        await Context.Tracing.StartAsync(new TracingStartOptions
        {
            Title = TestContext.CurrentContext.Test.ClassName + "." + TestContext.CurrentContext.Test.Name,
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
    }

    public static async Task StopTracing()
    {
        var isPassed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
        var tracesDir = Path.Combine(
            TestContext.CurrentContext.WorkDirectory,
            "playwright-traces",
            $"{TestContext.CurrentContext.Test.ClassName}.{TestContext.CurrentContext.Test.Name}.zip"
        );

        await Context.Tracing.StopAsync(new()
        {
            Path = isPassed ? null : tracesDir
        });
    }

    public static async Task<string> TakeScreenshotAsync(IPage page)
    {
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd-hhmm-ss");
        var screenshotName = $"{TestContext.CurrentContext.Test.Name}_{timestamp}.png";
        var screenshotsDir = Path.Combine(ExtentReportsHelper.ReportDirectory, "Screenshot");

        IoHelper.CreateFolderIfDoesNotExist(screenshotsDir);

        var screenshotPath = Path.Combine(screenshotsDir, screenshotName);

        await page.ScreenshotAsync(new PageScreenshotOptions
        {
            Path = screenshotPath,
            FullPage = true,
        });

        return screenshotPath;
    }


    private static async Task<IBrowser> CreateBrowserAsync()
    {
        var browserType = ConfigReader.GetConfigByName("browserType");

        var options = IsDebugging
            ? new BrowserTypeLaunchOptions
            {
                // todo: add LaunchOptions if needed
            }
            : new BrowserTypeLaunchOptions
            {
                Headless = false,
                Args = new[] { "--start-maximized" },
            };

        return browserType switch
        {
            BrowserType.Chromium => await PlaywrightObj.Chromium.LaunchAsync(options),
            BrowserType.Firefox => await PlaywrightObj.Chromium.LaunchAsync(options),

            _ => throw new ArgumentOutOfRangeException(nameof(browserType), "Unsupported browser")
        };
    }

    private static async Task<IPage> CreatePageAsync()
    {
        Context = await BrowserObj.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = ViewportSize.NoViewport
        });
        var page = await Context.NewPageAsync();

        if (IsDebugging == false)
        {
            await page.SetViewportSizeAsync(1920, 1080); // todo: change page size if needed 
        }

        return page;
    }
}