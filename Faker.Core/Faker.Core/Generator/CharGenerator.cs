namespace Faker.Core.Generator;

public class CharGenerator : IGenerator
{
    private readonly char[] _chars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    
    public object Generate(Type type)
    {
        return _chars[RandomS.Instance().Next(0, _chars.Length)];
    }
}