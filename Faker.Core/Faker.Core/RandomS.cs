namespace Faker.Core;

public static class RandomS
{
    private static readonly Random Random = new();

    public static Random Instance()
    {
        return Random;
    }
}