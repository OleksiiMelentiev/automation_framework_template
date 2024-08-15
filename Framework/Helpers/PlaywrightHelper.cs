using Microsoft.Playwright;

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

    public static async Task<IPage> GetPageAsync()
    {
        if (Page != null)
        {
            return Page;
        }

        PlaywrightObj = await Playwright.CreateAsync();
        BrowserObj = await CreateBrowserAsync();
        Page = await CreatePageAsync();
        return Page;
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