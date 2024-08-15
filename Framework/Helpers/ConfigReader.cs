using Microsoft.Extensions.Configuration;

namespace Framework.Helpers;

public class ConfigReader
{
    private static readonly IConfiguration Config;
    private static readonly IConfiguration ConfigLocal;

    static ConfigReader()
    {
        Config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        ConfigLocal = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
            .Build();
    }


    public static string GetConfigByName(string settingName)
    {
        var envValue = Environment.GetEnvironmentVariable(settingName);
        if (string.IsNullOrEmpty(envValue) == false)
        {
            return envValue;
        }

        var localValue = ConfigLocal[settingName];
        if (string.IsNullOrEmpty(localValue) == false)
        {
            return localValue;
        }

        return Config[settingName] ?? throw new ArgumentException($"Can't read '{settingName}' config");
    }
    
    public static string GetReportDir()
    {
        var dir = GetConfigByName("reportDirectory");

        if (string.IsNullOrEmpty(dir))
        {
            var projDir = IoHelper.GetProjectDirectory();
            dir = Path.Combine(projDir, "TestResult");
        }

        IoHelper.CreateFolderIfDoesNotExist(dir);

        return dir;
    }
}