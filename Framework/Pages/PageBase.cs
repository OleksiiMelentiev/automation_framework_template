using Framework.Helpers;
using Microsoft.Playwright;

namespace Framework.Pages;

public abstract class PageBase
{
    protected const string BaseUrl = "https://demoqa.com";
    protected abstract string Url { get; }

    protected IPage Page { get; private set; } = PlaywrightHelper.GetPage();


    public async Task Open()
    {
        await Page.GotoAsync(Url);
    }
}