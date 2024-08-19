using Microsoft.Playwright;

namespace Framework.Pages;

public class HomePage : PageBase
{
    private ILocator ElementsCard => Page.Locator("xpath=//h5[contains(text(), 'Elements')]/../../..");
    protected override string Url => BaseUrl;

    public async Task GoToElementsAsync()
    {
        await ElementsCard.ClickAsync();
    }
}