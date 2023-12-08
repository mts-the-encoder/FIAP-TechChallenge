using Bogus;
using Bogus.Extensions.Brazil;
using Communication.Requests;

namespace Utils.Requests;

public class UserRequestBuilder
{
    public static UserRequest Build(int passwordLength = 10)
    {
        return new Faker<UserRequest>()
            .RuleFor(x => x.Name, f => f.Person.FullName)
            .RuleFor(x => x.Email, f => f.Person.Email)
            .RuleFor(x => x.Phone,f => f.Phone.PhoneNumber("## ! ####-####").Replace("!", $"{f.Random.Int(1, 9)}"))
            .RuleFor(x => x.CNPJ,f => f.Company.Cnpj())
            .RuleFor(x => x.Password, f => f.Internet.Password(passwordLength));
    }
}