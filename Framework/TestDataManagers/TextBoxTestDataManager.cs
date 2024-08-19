using Framework.Models.Ui;

namespace Framework.TestDataManagers;

public class TextBoxTestDataManager : TestDataManagerBase
{
    public TextBoxUiModel GetRandom()
    {
        return new TextBoxUiModel()
        {
            CurrentAddress = GetRandomString(),
            Email = GetRandomString() + "@te.st",
            FullName = GetRandomString(),
            PermanentAddress = GetRandomString(),
        };
    }
}