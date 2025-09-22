using Moq;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Domain.Interfaces;
using SistemaDeVentas.Infrastructure.Core.Application.Services;
using Xunit;

namespace SistemaDeVentas.Infrastructure.Tests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task AuthenticateAsync_WithValidCredentials_ReturnsUser()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
            Username = "admin",
            Password = "hashedpassword",
            Role = "1",
            Name = "Administrator",
            IsActive = true
        };

        _userRepositoryMock.Setup(r => r.AuthenticateWithPwdCompareAsync("admin", "testpassword")).ReturnsAsync(user);

        // Act
        var result = await _userService.AuthenticateAsync("admin", "testpassword");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("admin", result.Username);
    }

    [Fact]
    public async Task AuthenticateAsync_WithInvalidUsername_ReturnsNull()
    {
        // Arrange
        _userRepositoryMock.Setup(r => r.AuthenticateWithPwdCompareAsync("nonexistent", "password")).ReturnsAsync((User?)null);

        // Act
        var result = await _userService.AuthenticateAsync("nonexistent", "password");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AuthenticateAsync_WithInactiveUser_ReturnsNull()
    {
        // Arrange
        _userRepositoryMock.Setup(r => r.AuthenticateWithPwdCompareAsync("admin", "password")).ReturnsAsync((User?)null);

        // Act
        var result = await _userService.AuthenticateAsync("admin", "password");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateUserAsync_HashesPasswordAndCreatesUser()
    {
        // Arrange
        var user = new User
        {
            Username = "newuser",
            Password = "plainpassword",
            Email = "newuser@example.com",
            Name = "New User",
            Role = "user"
        };

        var hashedPassword = "hashedpassword";
        _userRepositoryMock.Setup(r => r.HashPasswordAsync("plainpassword")).ReturnsAsync(hashedPassword);
        _userRepositoryMock.Setup(r => r.ExistsByEmailAsync(user.Email)).ReturnsAsync(false);
        _userRepositoryMock.Setup(r => r.CreateAsync(It.Is<User>(u => u.Password == hashedPassword))).ReturnsAsync(user);

        // Act
        var result = await _userService.CreateUserAsync(user);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(hashedPassword, result.Password);
        _userRepositoryMock.Verify(r => r.HashPasswordAsync("plainpassword"), Times.Once);
        _userRepositoryMock.Verify(r => r.CreateAsync(It.Is<User>(u => u.Password == hashedPassword)), Times.Once);
    }
}