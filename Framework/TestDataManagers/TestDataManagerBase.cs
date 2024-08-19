namespace Framework.TestDataManagers;

public class TestDataManagerBase
{
    public string GetRandomString(int length = 8, string charsToChoose = "")
    {
        var random = new Random();
        var chars = string.IsNullOrEmpty(charsToChoose)
            ? "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
            : charsToChoose;
        return new string(
            Enumerable
                .Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
    }
}