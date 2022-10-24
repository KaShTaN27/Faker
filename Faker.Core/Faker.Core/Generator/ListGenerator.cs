using System.Collections;
using Faker.Core.Exception;

namespace Faker.Core.Generator;

public class ListGenerator : IGenerator
{
    private readonly IFaker _faker;

    public ListGenerator(IFaker faker)
    {
        _faker = faker;
    }

    public object Generate(Type type)
    {
        var length = RandomS.Instance().Next(3, 8);
        var list = (IList) (Activator.CreateInstance(type)
                   ?? throw new ObjectInitializationException("Could not generate list"));
        for (var i = 0; i < length; i++)
            list.Add(_faker.Create(type.GetGenericArguments()[0]));
        return list;
    }
}