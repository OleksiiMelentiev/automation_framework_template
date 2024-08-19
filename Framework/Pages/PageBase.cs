using Framework.Helpers;
using Microsoft.Playwright;

namespace Framework.Pages;

public abstract class PageBase
{
    protected const string BaseUrl = "https://demoqa.com";
    protected abstract string Url { get; }

    protected IPage Page => PlaywrightHelper.GetPage();


    public async Task OpenAsync()
    {
        await Page.GotoAsync(Url);
    }
}