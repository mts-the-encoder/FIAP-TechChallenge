using Domain.Entities;
using Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryAccess.Repository;

public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByEmail(string email)
    {
        return await _context.Users.AnyAsync(x => x.Email.Equals(email));
    }

    public async Task Add(User user)
    {
        await _context.Users.AddAsync(user);
    }
}