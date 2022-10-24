using Faker.Core.Generator;

namespace Faker.Core;

public class Faker : IFaker
{
    private readonly GeneratorsFactory _generatorsFactory;
    private readonly Dictionary<Type, int> _nestedTypes = new();
    private const int NestedLevel = 3;

    public Faker()
    {
        _generatorsFactory = new GeneratorsFactory(this);
    }

    public T Create<T>()
    {
        return (T) Create(typeof(T));
    }

    public object Create(Type type)
    {
        AddNestedType(type);
        if (IsRecursionLoop(type))
            return null;
        var instance = _generatorsFactory.GetGenerator(type).Generate(type);
        RemoveNestedType(type);
        return instance;
    }

    private void AddNestedType(Type type)
    {
        if (_nestedTypes.ContainsKey(type))
            _nestedTypes[type]++;
        else
            _nestedTypes.Add(type, 1);
    }

    private void RemoveNestedType(Type type)
    {
        _nestedTypes[type]--;   
    }

    private bool IsRecursionLoop(Type type)
    {
        return _nestedTypes[type] >= NestedLevel;
    }
}