using Framework.Pages;
using Framework.Pages.Elements;

namespace Framework.Lists;

public class PagesList
{
    private HomePage? _home;
    public HomePage Home => _home ??= new HomePage();

    private ElementsPage? _elements;
    public ElementsPage Elements => _elements ??= new ElementsPage();

    private TextBoxPage? _textBox;
    public TextBoxPage TextBox => _textBox ??= new TextBoxPage();
}