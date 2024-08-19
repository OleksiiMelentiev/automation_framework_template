using Framework.Helpers;
using NUnit.Framework;

namespace Framework.Abstractions;

public class TestBase
{
    private readonly ExtentReportsManager _extentReports = ExtentReportsManager.Get();

    [SetUp]
    public void SetUpBase()
    {
        _extentReports.CreateTest(TestContext.CurrentContext.Test.Name, TestContext.CurrentContext.Test.ClassName);
    }
    
    [OneTimeTearDown]
    public void OneTimeTearDownBase()
    {
        _extentReports.EndReporting();
    }
    
    [TearDown]
    public void TearDownBase()
    {
        _extentReports.EndTest();
    }
}