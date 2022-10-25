using System.Reflection;
using Faker.Core.Exception;

namespace Faker.Core.Generator;

public class ObjectGenerator : IGenerator
{
    private readonly IFaker _faker;

    public ObjectGenerator(IFaker faker)
    {
        _faker = faker;
    }

    public object Generate(Type type)
    {
        return CreateObjectInstance(type);
    }

    private object CreateObjectInstance(Type type)
    {
        try
        {
            var typeConstructors = GetTypeConstructors(type);
            var instance = CreateByConstructor(typeConstructors)
                           ?? Activator.CreateInstance(type)
                           ?? throw new ObjectInitializationException("Could not initialize object");
            return InitializeObject(instance);
        }
        catch (System.Exception e)
        {
            throw new ObjectInitializationException(e.Message);
        }
    }

    private static IEnumerable<ConstructorInfo> GetTypeConstructors(Type type)
    {
        return type.GetConstructors()
            .OrderByDescending(c => c.GetParameters().Length)
            .ToList();
    }

    private object? CreateByConstructor(IEnumerable<ConstructorInfo> constructors)
    {
        foreach (var constructor in constructors)
        {
            try
            {
                var parameters = GetConstructorParameters(constructor);
                return constructor.Invoke(parameters);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
            }
        }

        return null;
    }

    private object[] GetConstructorParameters(ConstructorInfo constructor)
    {
        return constructor.GetParameters()
            .Select(p => _faker.Create(p.ParameterType)).ToArray();
    }

    private object InitializeObject(object instance)
    {
        var uninitializedMembers = GetUninitializedMembers(instance);
        InitializeMembers(uninitializedMembers, instance);
        return instance;
    }

    private void InitializeMembers(IEnumerable<MemberInfo> uninitializedMembers, object instance)
    {
        foreach (var member in uninitializedMembers)
        {
            try
            {
                SetMemberValue(member, instance);
            }
            catch (System.Exception exception)
            {
                throw new MemberInitializationException(exception.Message);
            }
        }
    }

    private static IEnumerable<MemberInfo> GetUninitializedMembers(object instance)
    {
        return instance.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance)
            .Where(m =>
                (m.MemberType.Equals(MemberTypes.Field) ||
                 m.MemberType.Equals(MemberTypes.Property)) &&
                GetMemberValue(m, instance).Equals(GetDefaultValue(m.DeclaringType!)));
    }

    private static object GetMemberValue(MemberInfo memberInfo, object value)
    {
        return (memberInfo.MemberType.Equals(MemberTypes.Field)
            ? ((FieldInfo)memberInfo).GetValue(value)
            : ((PropertyInfo)memberInfo).GetValue(value))!;
    }

    private void SetMemberValue(MemberInfo memberInfo, object value)
    {
        if (memberInfo.MemberType.Equals(MemberTypes.Field))
            ((FieldInfo)memberInfo).SetValue(value, _faker.Create(value.GetType()));
        else
            ((PropertyInfo)memberInfo).SetValue(value, _faker.Create(value.GetType()));
    }

    public static object? GetDefaultValue(Type type)
    {
        return type.IsValueType ? Activator.CreateInstance(type) : null;
    }
}