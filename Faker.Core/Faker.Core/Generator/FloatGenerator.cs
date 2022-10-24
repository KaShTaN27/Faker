namespace Faker.Core.Generator;

public class FloatGenerator : IGenerator
{
    public object Generate(Type type)
    {
        return RandomS.Instance().NextSingle() * 10;
    }
}