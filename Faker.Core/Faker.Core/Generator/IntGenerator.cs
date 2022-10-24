namespace Faker.Core.Generator;

public class IntGenerator : IGenerator
{
    public object Generate(Type type)
    {
        return RandomS.Instance().Next(1, int.MaxValue);
    }
}