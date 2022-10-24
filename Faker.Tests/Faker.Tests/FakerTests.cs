using Faker.Core;
using Faker.Core.Generator;
using Faker.Tests.Model;

namespace Faker.Tests;

public class FakerTests
{
    private IFaker _faker; 
    
    [SetUp]
    public void Setup()
    {
        _faker = new Core.Faker();
    }

    private static IEnumerable<TestCaseData> DataTypes()
    {
        yield return new TestCaseData(typeof(bool));
        yield return new TestCaseData(typeof(byte));
        yield return new TestCaseData(typeof(char));
        yield return new TestCaseData(typeof(double));
        yield return new TestCaseData(typeof(float));
        yield return new TestCaseData(typeof(int));
        yield return new TestCaseData(typeof(long));
        yield return new TestCaseData(typeof(short));
        yield return new TestCaseData(typeof(string));
    }

    [Test, TestCaseSource(nameof(DataTypes))]
    public void CreateValueThatNotEqualsDefault(Type type)
    {
        Assert.That(_faker.Create(type), Is.Not.EqualTo(ObjectGenerator.GetDefaultValue(type)));
    }

    [Test]
    public void CreateTypeWithRecursionInside()
    {
        var user = _faker.Create<User>();
        Assert.That(user.GetUser().GetUser(), Is.Null);
    }

    [Test]
    public void CreateObjectWithNotNullValues()
    {
        var user = _faker.Create<User>();
        Assert.Multiple(() =>
        {
            Assert.That(user.IsBlocked, Is.Not.EqualTo(ObjectGenerator.GetDefaultValue(user.IsBlocked.GetType())));
            Assert.That(user.Balance, Is.Not.EqualTo(ObjectGenerator.GetDefaultValue(user.Balance.GetType())));
            Assert.That(user.GetUser(), Is.Not.Null);
        });
    }

    [Test]
    public void CreateTypeByConstructorWithMostParameters()
    {
        var user = _faker.Create<User>();
        Assert.That(user.Balance, Is.EqualTo(user.Salary));
    }
}