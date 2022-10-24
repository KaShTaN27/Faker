namespace Faker.Core.Generator;

public class GeneratorsFactory
{
    public GeneratorsFactory(IFaker faker)
    {
        _objectGenerator = new ObjectGenerator(faker);
        _generators = new Dictionary<Type, IGenerator>
        {
            { typeof(bool), new BoolGenerator() },
            { typeof(byte), new ByteGenerator() },
            { typeof(char), new CharGenerator() },
            { typeof(float), new FloatGenerator() },
            { typeof(int), new IntGenerator() },
            { typeof(double), new DoubleGenerator() },
            { typeof(long), new LongGenerator() },
            { typeof(short), new ShortGenerator() },
            { typeof(string), new StringGenerator() },
            { typeof(DateTime), new DateTimeGenerator() },
            { typeof(List<>), new ListGenerator(faker)}
        };
    }

    private readonly IGenerator _objectGenerator;
    private readonly Dictionary<Type, IGenerator> _generators;

    public IGenerator GetGenerator(Type type)
    {
        if (type.IsGenericType)
            type = type.GetGenericTypeDefinition();
        return _generators.TryGetValue(type, out var generator) ? generator : _objectGenerator;
    }
}