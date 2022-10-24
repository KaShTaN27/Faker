namespace Faker.Tests.Model;

public class User
{
    public bool IsBlocked;
    public double Balance;
    public double Salary;
    private readonly User _user;

    public User(bool isBlocked, double balance, User user)
    {
        this.IsBlocked = isBlocked;
        this.Balance = balance;
        this._user = user;
        this.Salary = 500;
    }

    public User(bool isBlocked, double balance, double salary, User user)
    {
        IsBlocked = isBlocked;
        Balance = salary;
        Salary = salary;
        _user = user;
    }

    public User GetUser() => _user;
}