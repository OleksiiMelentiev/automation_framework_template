using Framework.Abstractions;
using Framework.Helpers;
using Framework.Models.Api;

namespace Tests.Api;

public class Example : TestBase
{
    [Test]
    public async Task ExamplePassed()
    {
        // preconditions
        var exampleModel = TestData.ApiExample.GetExampleModel();

        // steps:
        var response = await Clients.Example.PostAsync(exampleModel);
        var createdModel = await HttpHelper.GetModelFromResponseAsync<ExampleModel>(response);
        var getResponse = await Clients.Example.GetAsync(createdModel.Id);
        var getModel = await HttpHelper.GetModelFromResponseAsync<ExampleModel>(getResponse);

        // check results
        Assert.Multiple(() =>
        {
            Assert.That(createdModel.Id, Is.EqualTo(exampleModel.Id));
            Assert.That(createdModel.Name, Is.EqualTo(exampleModel.Name));
            Assert.That(getModel.Id, Is.EqualTo(exampleModel.Id));
            Assert.That(getModel.Name, Is.EqualTo(exampleModel.Name));
        });
    }


    [Test]
    public async Task ExampleFailed()
    {
        // preconditions
        var exampleModel = TestData.ApiExample.GetExampleModel();

        // steps:
        var response = await Clients.Example.PostAsync(exampleModel);
        var createdModel = await HttpHelper.GetModelFromResponseAsync<ExampleModel>(response);
        var getResponse = await Clients.Example.GetAsync(createdModel.Id);
        var getModel = await HttpHelper.GetModelFromResponseAsync<ExampleModel>(getResponse);

        // check results
        Assert.Multiple(() =>
        {
            Assert.That(createdModel.Id, Is.EqualTo(Guid.NewGuid()));
            Assert.That(createdModel.Name, Is.EqualTo("incorrect name"));
            Assert.That(getModel.Id, Is.EqualTo(Guid.NewGuid()));
            Assert.That(getModel.Name, Is.EqualTo("incorrect name"));
        });
    }
}