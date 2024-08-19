namespace Framework.Helpers;

public static class MyLogger
{
    public static void LogInfo(string message)
    {
        // todo: log to other places if needed
        
        Console.WriteLine(message);
        
        var extentHelper = ExtentReportsManager.Get();
        extentHelper.LogInfo(message);
    }
}