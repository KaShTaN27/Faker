namespace Faker.Core.Generator;

public class ByteGenerator : IGenerator
{
    public object Generate(Type type)
    {
        return (byte)RandomS.Instance().Next(1, byte.MaxValue);
    }
}