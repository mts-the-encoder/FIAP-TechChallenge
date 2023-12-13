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

    public async Task<User> Login(string email, string password)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email.Equals(email) && x.Password.Equals(password));
    }

    public async Task Add(User user)
    {
        await _context.Users.AddAsync(user);
    }
}