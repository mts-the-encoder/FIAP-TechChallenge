﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.RepositoryAccess;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<VariableIncome> VariableIncomes { get; set; }
    public DbSet<FixedIncome> FixedIncomes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}