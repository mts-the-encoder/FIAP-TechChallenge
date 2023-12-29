using Domain.Entities;
using Infrastructure.RepositoryAccess;
using Utils.Entities;

namespace WebApi.Tests;

public static class ContextSeedInMemory
{
    public static (User user, string password) Seed(AppDbContext context)
    {
        var (user, password) = UserBuilder.Build();
        var variableIncome = VariableIncomeBuilder.Build();
        var fixedIncome = FixedIncomeBuilder.Build();

        context.Users.Add(user);
        context.VariableIncomes.Add(variableIncome);
        context.FixedIncomes.Add(fixedIncome);

        context.SaveChanges();

        return (user, password);
    }
}