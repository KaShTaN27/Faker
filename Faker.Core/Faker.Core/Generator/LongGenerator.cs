namespace Faker.Core.Generator;

public class LongGenerator : IGenerator
{
    public object Generate(Type type)
    {
        return RandomS.Instance().NextInt64(1, long.MaxValue);
    }
}