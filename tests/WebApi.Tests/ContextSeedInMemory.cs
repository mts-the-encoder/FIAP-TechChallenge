using Domain.Entities;
using Infrastructure.RepositoryAccess;
using Utils.Entities;

namespace WebApi.Tests;

public static class ContextSeedInMemory
{
    public static (User user, string password) Seed(AppDbContext context)
    {
        var (user, password) = UserBuilder.Build();

        context.Users.Add(user);

        context.SaveChanges();

        return (user, password);
    }
}