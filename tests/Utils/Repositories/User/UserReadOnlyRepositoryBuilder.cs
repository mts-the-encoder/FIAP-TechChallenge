using Domain.Repositories.User;
using Moq;

namespace Utils.Repositories.User;

public class UserReadOnlyRepositoryBuilder
{
    private static UserReadOnlyRepositoryBuilder _instance;
    private readonly Mock<IUserReadOnlyRepository> _repository;

    private UserReadOnlyRepositoryBuilder()
    {
        _repository ??= new Mock<IUserReadOnlyRepository>();
    }

    public static UserReadOnlyRepositoryBuilder Instance()
    {
        _instance = new UserReadOnlyRepositoryBuilder();
        return _instance;
    }

    public UserReadOnlyRepositoryBuilder ExistsByEmail(string email)
    {
        if (!string.IsNullOrWhiteSpace(email))
            _repository.Setup(i => i.ExistsByEmail(email)).ReturnsAsync(true);
        
        return this;
    }

    public IUserReadOnlyRepository Build()
    {
        return _repository.Object;
    }
}