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
            .RuleFor(x => x.Dividends,_ => 0.50)
            .RuleFor(x => x.MinimumInvestment, 10.0)
            .RuleFor(x => x.InvestmentVariableType,f => f.PickRandom<InvestmentVariableType>())
            .RuleFor(x => x.Sector,f => f.PickRandom<Sector>());

        return vIncomeCreated;
    }
}