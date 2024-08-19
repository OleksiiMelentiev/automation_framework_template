namespace Framework.Models.Api;

public class ExampleModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = default!;
}