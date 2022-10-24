namespace Faker.Core.Generator;

public interface IGenerator
{
    object Generate(Type type);
}