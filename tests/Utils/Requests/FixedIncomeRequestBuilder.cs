using Bogus;
using Communication.Enum;
using Communication.Requests;

namespace Utils.Requests;

public class FixedIncomeRequestBuilder
{
    public static FixedIncomeRequest Build()
    {
        var fIncomeCreated = new Faker<FixedIncomeRequest>()
            .RuleFor(x => x.Sender,f => f.Company.CompanyName())
            .RuleFor(x => x.Profitability,f => f.Company.CompanySuffix())
            .RuleFor(x => x.MinimumInvestment,f => f.Random.Double(50,1000))
            .RuleFor(x => x.InvestmentFixedType,f => f.PickRandom<InvestmentFixedType>())
            .RuleFor(x => x.IR,f => f.Random.Int());

        return fIncomeCreated;
    }
}