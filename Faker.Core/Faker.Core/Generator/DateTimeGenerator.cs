namespace Faker.Core.Generator;

public class DateTimeGenerator : IGenerator
{
    public object Generate(Type type)
    {
        return new DateTime(RandomS.Instance().NextInt64());
    }
}