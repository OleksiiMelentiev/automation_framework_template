namespace Framework.Helpers;

public static class IoHelper
{
    public static void CreateFolderIfDoesNotExist(string folderPath)
    {
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
    }
    
    public static string GetProjectDirectory()
    {
        var currentDir = Directory.GetCurrentDirectory();
        var currentDirInfo = new DirectoryInfo(currentDir) ?? throw new IOException("Cant get report dir");

        return currentDirInfo.Parent?.Parent?.Parent?.Parent?.FullName
               ?? throw new IOException("Cant get report dir");
    }
}