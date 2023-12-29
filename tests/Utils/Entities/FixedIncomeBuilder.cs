using Bogus;
using Domain.Entities;
using Domain.Enums;

namespace Utils.Entities;

public class FixedIncomeBuilder
{
    public static FixedIncome Build()
    {
        var fIncomeCreated = new Faker<FixedIncome>()
            .RuleFor(x => x.Id,f => f.Random.Int())
            .RuleFor(x => x.UserId,f => f.Random.Int())
            .RuleFor(x => x.Sender,f => f.Company.CompanyName())
            .RuleFor(x => x.Profitability,f => f.Company.CompanySuffix())
            .RuleFor(x => x.MinimumInvestment,f => f.Random.Double(50, 1000))
            .RuleFor(x => x.InvestmentFixedType,f => f.PickRandom<InvestmentFixedType>())
            .RuleFor(x => x.IR,f => f.Random.Int())
            .RuleFor(x => x.MaturityDate, f => f.Date.Recent());

        return fIncomeCreated;
    }
}