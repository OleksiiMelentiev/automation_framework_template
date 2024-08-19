using Framework.TestDataManagers;

namespace Framework.Lists;

public class TestDataManagersList
{
    private ApiExampleTestDataManager? _apiExample;
    public ApiExampleTestDataManager ApiExample => _apiExample ??= new ApiExampleTestDataManager();

    private TextBoxTestDataManager? _textBox;
    public TextBoxTestDataManager TextBox => _textBox ??= new TextBoxTestDataManager();
}