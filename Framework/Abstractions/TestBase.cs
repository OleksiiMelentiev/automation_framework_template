using Framework.Helpers;
using Framework.Lists;
using NUnit.Framework;

namespace Framework.Abstractions;

public class TestBase
{
    protected readonly ExtentReportsManager ExtentReports = ExtentReportsManager.Get();

    protected readonly ClientsList Clients = new();
    protected readonly TestDataManagersList TestData = new();

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