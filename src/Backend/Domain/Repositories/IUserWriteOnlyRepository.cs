using Domain.Entities;

namespace Domain.Repositories;

public interface IUserWriteOnlyRepository
{
    Task Add(User user);
}