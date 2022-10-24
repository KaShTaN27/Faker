namespace Faker.Core.Generator;

public class DoubleGenerator : IGenerator
{
    public object Generate(Type type)
    {
        return RandomS.Instance().NextDouble() + 1 * 10;
    }
}