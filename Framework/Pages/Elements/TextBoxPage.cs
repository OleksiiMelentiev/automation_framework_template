using Framework.Models.Ui;
using Microsoft.Playwright;

namespace Framework.Pages.Elements;

public class TextBoxPage : PageBase
{
    protected override string Url => BaseUrl + "/text-box";

    private ILocator CurrenAddressInput =>
        Page.Locator("xpath=//*[@id='currentAddress-wrapper']//*[@id='currentAddress']");

    private ILocator CurrenAddressOutput => Page.Locator("xpath=//*[@id='output']//*[@id='currentAddress']");
    private ILocator EmailInput => Page.Locator("xpath=//*[@id='userEmail']");
    private ILocator EmailOutput => Page.Locator("xpath=//*[@id='output']//*[@id='email']");
    private ILocator FullNameInput => Page.Locator("xpath=//*[@id='userName']");
    private ILocator FullNameOutput => Page.Locator("xpath=//*[@id='name']");
    private ILocator PermanentAddressInput => Page.Locator("xpath=//*[@id='permanentAddress-wrapper']//*[@id='permanentAddress']");
    private ILocator PermanentAddressOutput => Page.Locator("xpath=//*[@id='output']//*[@id='permanentAddress']");


    public async Task<TextBoxUiModel> GetFormOutputAsync()
    {
        var nameText = await FullNameOutput.TextContentAsync();
        var emailText = await EmailOutput.TextContentAsync();
        var currentAddressText = await CurrenAddressOutput.TextContentAsync();
        var permanentAddressText = await PermanentAddressOutput.TextContentAsync();

        return new TextBoxUiModel()
        {
            FullName = nameText?.Split(":")[1].Trim(),
            Email = emailText?.Split(":")[1].Trim(),
            CurrentAddress = currentAddressText?.Split(":")[1].Trim(),
            PermanentAddress = permanentAddressText?.Split(":")[1].Trim(),
        };
    }

    public async Task FillInTextBoxFormAsync(TextBoxUiModel textBox)
    {
        if (textBox.FullName != null)
        {
            await FullNameInput.FillAsync(textBox.FullName);
        }

        if (textBox.Email != null)
        {
            await EmailInput.FillAsync(textBox.Email);
        }

        if (textBox.CurrentAddress != null)
        {
            await CurrenAddressInput.FillAsync(textBox.CurrentAddress);
        }

        if (textBox.PermanentAddress != null)
        {
            await PermanentAddressInput.FillAsync(textBox.PermanentAddress);
        }
    }
}