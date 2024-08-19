using Framework.Helpers;
using NUnit.Framework;

namespace Framework.Abstractions;

public class TestBase
{
    protected readonly ExtentReportsManager ExtentReports = ExtentReportsManager.Get();

    [SetUp]
    public void SetUpBase()
    {
        ExtentReports.CreateTest(TestContext.CurrentContext.Test.Name, TestContext.CurrentContext.Test.ClassName);
    }
    
    [OneTimeTearDown]
    public void OneTimeTearDownBase()
    {
        ExtentReports.EndReporting();
    }
    
    [TearDown]
    public void TearDownBase()
    {
        ExtentReports.EndTest();
    }
}