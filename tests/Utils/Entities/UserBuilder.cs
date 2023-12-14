using Bogus;
using Bogus.Extensions.Brazil;
using Domain.Entities;
using Utils.PasswordEncryptor;

namespace Utils.Entities;

public class UserBuilder
{
    public static (User user, string password) Build()
    {
        var password = string.Empty;

        var userCreated = new Faker<User>()
            .RuleFor(x => x.Id, _ => 1)
            .RuleFor(x => x.Name,f => f.Person.FullName)
            .RuleFor(x => x.Email,f => f.Person.Email)
            .RuleFor(x => x.Phone,f => f.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{f.Random.Int(1, 9)}"))
            .RuleFor(x => x.CNPJ,f => f.Company.Cnpj())
            .RuleFor(x => x.Password, f =>
            {
                password = f.Internet.Password();

                return PasswordEncryptorBuilder.Instance().Encrypt(password);
            });

        return (userCreated, password);
    }
}