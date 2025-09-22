using Moq;
using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.WinUI.Services;
using Xunit;

namespace SistemaDeVentas.WinUI.Tests;

public class AuthenticationServiceTests
{
    private readonly Mock<IAuthService> _authServiceMock;
    private readonly AuthenticationService _authenticationService;

    public AuthenticationServiceTests()
    {
        _authServiceMock = new Mock<IAuthService>();
        _authenticationService = new AuthenticationService(_authServiceMock.Object);
    }

    [Fact]
    public async Task LoginAsync_WithValidCredentials_ReturnsSuccess()
    {
        // Arrange - Using real user data structure
        var realUser = new User
        {
            Id = 2,
            Username = "admin",
            Password = "hashedpassword",
            Role = "1",
            Name = null,
            IsActive = true
        };

        _authServiceMock.Setup(a => a.AuthenticateAsync("admin", "password")).ReturnsAsync(realUser);

        // Act
        var result = await _authenticationService.LoginAsync("admin", "password");

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.User);
        Assert.Equal("admin", result.User.Username);
        Assert.True(_authenticationService.IsAuthenticated);
        Assert.Equal(realUser, _authenticationService.CurrentUser);
    }

    [Fact]
    public async Task LoginAsync_WithInvalidCredentials_ReturnsFailure()
    {
        // Arrange
        _authServiceMock.Setup(a => a.AuthenticateAsync("admin", "wrongpassword")).ReturnsAsync((User?)null);

        // Act
        var result = await _authenticationService.LoginAsync("admin", "wrongpassword");

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Null(result.User);
        Assert.Contains("Credenciales inválidas", result.ErrorMessage);
        Assert.False(_authenticationService.IsAuthenticated);
        Assert.Null(_authenticationService.CurrentUser);
    }

    [Fact]
    public async Task LoginAsync_WithException_ReturnsFailure()
    {
        // Arrange
        _authServiceMock.Setup(a => a.AuthenticateAsync("admin", "password"))
            .ThrowsAsync(new Exception("Database connection failed"));

        // Act
        var result = await _authenticationService.LoginAsync("admin", "password");

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Null(result.User);
        Assert.Contains("Error de autenticación", result.ErrorMessage);
        Assert.False(_authenticationService.IsAuthenticated);
    }

    [Fact]
    public async Task LogoutAsync_SetsUserToNull()
    {
        // Arrange
        var user = new User { Id = 1, Username = "test", IsActive = true };
        _authServiceMock.Setup(a => a.AuthenticateAsync("test", "pass")).ReturnsAsync(user);
        await _authenticationService.LoginAsync("test", "pass");

        // Act
        var result = await _authenticationService.LogoutAsync();

        // Assert
        Assert.True(result);
        Assert.False(_authenticationService.IsAuthenticated);
        Assert.Null(_authenticationService.CurrentUser);
    }

    [Fact]
    public async Task GetCurrentUserAsync_ReturnsCurrentUser()
    {
        // Arrange
        var user = new User { Id = 1, Username = "test", IsActive = true };
        _authServiceMock.Setup(a => a.AuthenticateAsync("test", "pass")).ReturnsAsync(user);
        await _authenticationService.LoginAsync("test", "pass");

        // Act
        var result = await _authenticationService.GetCurrentUserAsync();

        // Assert
        Assert.Equal(user, result);
    }

    [Fact]
    public async Task IsAuthenticated_WhenLoggedIn_ReturnsTrue()
    {
        // Arrange
        var user = new User { Id = 1, Username = "test", IsActive = true };
        _authServiceMock.Setup(a => a.AuthenticateAsync("test", "pass")).ReturnsAsync(user);

        // Act - Login first
        await _authenticationService.LoginAsync("test", "pass");

        // Assert
        Assert.True(_authenticationService.IsAuthenticated);
    }

    [Fact]
    public void IsAuthenticated_WhenNotLoggedIn_ReturnsFalse()
    {
        // Assert
        Assert.False(_authenticationService.IsAuthenticated);
    }
}