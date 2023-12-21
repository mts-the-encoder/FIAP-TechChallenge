using Bogus;
using Communication.Enum;
using Communication.Requests;
using Domain.Entities;

namespace Utils.Requests;

public class VariableIncomeRequestBuilder
{
    public static VariableIncomeRequest Build()
    {
        var vIncomeCreated = new Faker<VariableIncomeRequest>()
            .RuleFor(x => x.Sender,f => f.Company.CompanyName())
            .RuleFor(x => x.Name,f => f.Company.CompanySuffix())
            .RuleFor(x => x.Dividends,f => f.Random.Double(0.20, 20.0))
            .RuleFor(x => x.MinimumInvestment,f => f.Random.Double(1, 1000))
            .RuleFor(x => x.InvestmentVariableType,f => f.PickRandom<InvestmentVariableType>())
            .RuleFor(x => x.Sector,f => f.PickRandom<Sector>());

        return vIncomeCreated;
    }
}