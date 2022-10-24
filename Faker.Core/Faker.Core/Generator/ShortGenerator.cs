namespace Faker.Core.Generator;

public class ShortGenerator : IGenerator
{
    public object Generate(Type type)
    {
        return (short)RandomS.Instance().Next(1, short.MaxValue);
    }
}