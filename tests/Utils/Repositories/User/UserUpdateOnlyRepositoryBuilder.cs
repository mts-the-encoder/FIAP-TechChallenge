﻿using Domain.Repositories.User;
using Moq;

namespace Utils.Repositories.User;

public class UserUpdateOnlyRepositoryBuilder
{
    private static UserUpdateOnlyRepositoryBuilder _instance;
    private readonly Mock<IUserUpdateOnlyRepository> _repository;

    private UserUpdateOnlyRepositoryBuilder()
    {
        _repository ??= new Mock<IUserUpdateOnlyRepository>();
    }

    public static UserUpdateOnlyRepositoryBuilder Instance()
    {
        _instance = new UserUpdateOnlyRepositoryBuilder();
        return _instance;
    }

    public UserUpdateOnlyRepositoryBuilder GetById(Domain.Entities.User user)
    {
        _repository.Setup(x => x.GetById(user.Id)).ReturnsAsync(user);
        return this;
    }

    public IUserUpdateOnlyRepository Build()
    {
        return _repository.Object;
    }
}