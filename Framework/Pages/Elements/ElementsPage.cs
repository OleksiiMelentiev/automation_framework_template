using Microsoft.Playwright;

namespace Framework.Pages.Elements;

public class ElementsPage : PageBase
{
    private ILocator TextBoxBtn => Page.Locator("xpath=//*[contains(text(), 'Text Box')]/..");
    
    protected override string Url => BaseUrl + "/elements";
    
    
    public async Task GoToTextBoxAsync()
    {
        await TextBoxBtn.ClickAsync();
    }
}