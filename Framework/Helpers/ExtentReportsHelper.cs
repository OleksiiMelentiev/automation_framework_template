using System.Collections.Concurrent;
using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace Framework.Helpers;

public class ExtentReportsHelper
{
    private static readonly object Padlock = new();
    private static readonly ConcurrentDictionary<string, ExtentTest> ExtentMap = new();
    private static ExtentReports? _extentReports;
    
    public static readonly string ReportDirectory = ConfigReader.GetReportDir();

    private static ExtentReportsHelper? _instance;

    private static ExtentReportsHelper Instance
    {
        get
        {
            lock (Padlock)
            {
                return _instance ??= new ExtentReportsHelper();
            }
        }
    }


    public void CreateTest(string testName, string? testCategory)
    {
        lock (Padlock)
        {
            var extentTest = GetReporting().CreateTest(testName);
            if (testCategory != null)
            {
                extentTest.AssignCategory(testCategory);
            }

            ExtentMap.TryAdd(testName, extentTest);
        }
    }

    public static ExtentReportsHelper Get()
    {
        return Instance;
    }

    public void EndReporting()
    {
        lock (Padlock)
        {
            GetReporting().Flush();
        }
    }

    public void EndTest()
    {
        var status = TestContext.CurrentContext.Result.Outcome.Status;

        if (status == TestStatus.Passed)
        {
            GetExtentTest().Pass();
        }
        else
        {
            var exception = string.IsNullOrEmpty(TestContext.CurrentContext.Result.Message)
                ? string.Empty
                : $"<pre>{TestContext.CurrentContext.Result.Message}</pre>";
            exception += $"<pre>{TestContext.CurrentContext.Result.StackTrace}</pre><br>";
            GetExtentTest().Fail(exception);
        }
        
        lock (Padlock)
        {
            var testName = TestContext.CurrentContext.Test.Name;
            ExtentMap.TryRemove(testName, out _);
        }
    }
    
    public void LogInfo(string info, string filePath)
    {
        lock (Padlock)
        {
            GetExtentTest().Info(info).AddScreenCaptureFromPath(filePath);
        }
    }

    public void LogInfo(string message)
    {
        lock (Padlock)
        {
            GetExtentTest().Log(Status.Info, message);
        }
    }
    
    public void LogScreenshot(string info, string imagePath)
    {
        lock (Padlock)
        {
            GetExtentTest().Info(info, MediaEntityBuilder.CreateScreenCaptureFromBase64String(imagePath).Build());
        }
    }


    private ExtentTest GetExtentTest()
    {
        lock (Padlock)
        {
            var testName = TestContext.CurrentContext.Test.Name;
            return ExtentMap[testName];
        }
    }

    private ExtentReports GetReporting()
    {
        lock (Padlock)
        {
            if (_extentReports == null)
            {
                Console.WriteLine($"Creating test results in ${ReportDirectory}");
                var reportFilePath = Path.Combine(ReportDirectory, "index.html");
                var reporter = new AventStack.ExtentReports.Reporter.ExtentSparkReporter(reportFilePath);
                _extentReports = new ExtentReports();
                _extentReports.AttachReporter(reporter);
            }

            return _extentReports;
        }
    }
}