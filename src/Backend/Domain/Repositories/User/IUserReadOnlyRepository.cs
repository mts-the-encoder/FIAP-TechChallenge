﻿namespace Domain.Repositories.User;
public interface IUserReadOnlyRepository
{
    Task<bool> ExistsByEmail(string email);
}