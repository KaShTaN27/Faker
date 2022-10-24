namespace Faker.Core;

public interface IFaker
{
    T Create<T>();
    object Create(Type type);
}