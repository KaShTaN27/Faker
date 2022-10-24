using System.Text;

namespace Faker.Core.Generator;

public class StringGenerator : IGenerator
{
    private readonly char[] _chars = "1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
    
    public object Generate(Type type)
    {
        var sb = new StringBuilder();
        var length = RandomS.Instance().Next(3, 15);
        for (var i = 0; i < length; i++)
            sb.Append(_chars[RandomS.Instance().Next(0, _chars.Length)]);
        return sb.ToString();
    }
}