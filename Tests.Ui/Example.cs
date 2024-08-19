using Framework.Abstractions;

namespace Tests.Ui;

public class Example : UiTestBase
{
    [Test]
    public async Task ExamplePassed()
    {
        // preconditions
        var textBoxForm = TestData.TextBox.GetRandom();
        
        // steps
        await Pages.Home.OpenAsync();
        await Pages.Home.GoToElementsAsync();
        await Pages.Elements.GoToTextBoxAsync();
        await Pages.TextBox.FillInTextBoxFormAsync(textBoxForm);
        var output = await Pages.TextBox.GetFormOutputAsync();

        // check results
        Assert.Multiple(() =>
        {
            Assert.That(output.FullName, Is.EqualTo(textBoxForm.FullName));
            Assert.That(output.Email, Is.EqualTo(textBoxForm.Email));
            Assert.That(output.CurrentAddress, Is.EqualTo(textBoxForm.CurrentAddress));
            Assert.That(output.PermanentAddress, Is.EqualTo(textBoxForm.PermanentAddress));
        });
    }

    [Test]
    public async Task ExampleFailed()
    {
        // preconditions
        var textBoxForm = TestData.TextBox.GetRandom();
        
        // steps
        await Pages.Home.OpenAsync();
        await Pages.Home.GoToElementsAsync();
        await Pages.Elements.GoToTextBoxAsync();
        await Pages.TextBox.FillInTextBoxFormAsync(textBoxForm);
        var output = await Pages.TextBox.GetFormOutputAsync();

        // check results
        Assert.Multiple(() =>
        {
            Assert.That(output.FullName, Is.EqualTo("failed name"));
            Assert.That(output.Email, Is.EqualTo("failed email"));
            Assert.That(output.CurrentAddress, Is.EqualTo("failed address"));
            Assert.That(output.PermanentAddress, Is.EqualTo("failed address"));
        });
    }
}