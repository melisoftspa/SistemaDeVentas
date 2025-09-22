using Microsoft.EntityFrameworkCore;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Infrastructure.Data;
using SistemaDeVentas.Infrastructure.Data.Repositories;
using Xunit;

namespace SistemaDeVentas.Infrastructure.Tests;

public class UserRepositoryTests : IDisposable
{
    private readonly SalesSystemDbContext _context;
    private readonly UserRepository _repository;

    public UserRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<SalesSystemDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new SalesSystemDbContext(options);
        _repository = new UserRepository(_context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    [Fact]
    public async Task GetByUsernameAsync_WithExistingUsername_ReturnsUser()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = 1, Username = "admin", Password = "", Role = "1", Name = "Administrator", IsActive = true },
            new User { Id = 2, Username = "jp", Password = "", Role = "3", Name = "Juan Meliano", IsActive = true },
            new User { Id = 3, Username = "Vendedor", Password = "vendedor", Role = "3", Name = "LA BODEGUITA", IsActive = true },
        };

        _context.Users.AddRange(users);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetByUsernameAsync("admin");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("admin", result.Username);
        Assert.Equal("1", result.Role);
    }

    [Fact]
    public async Task GetByUsernameAsync_WithNonExistentUsername_ReturnsNull()
    {
        // Arrange
        var users = new List<User>
        {
            new User { Id = 1, Username = "admin", Password = "hashedpassword", Role = "1", Name = "Administrator", IsActive = true }
        };

        _context.Users.AddRange(users);
        await _context.SaveChangesAsync();

        // Act
        var result = await _repository.GetByUsernameAsync("nonexistent");

        // Assert
        Assert.Null(result);
    }
}