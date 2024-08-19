using Framework.TestDataManagers;

namespace Framework.Lists;

public class TestDataManagersList
{
    private TextBoxTestDataManager? _textBox;
    public TextBoxTestDataManager TextBox => _textBox ??= new TextBoxTestDataManager();
}