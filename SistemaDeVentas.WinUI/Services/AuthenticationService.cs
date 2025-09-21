using System;
using System.Threading.Tasks;
using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Infrastructure.Core.Application.Services;

namespace SistemaDeVentas.WinUI.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IAuthService _authService;
    private User? _currentUser;

    public AuthenticationService(IAuthService authService)
    {
        _authService = authService;
    }

    public bool IsAuthenticated => _currentUser != null;
    public User? CurrentUser => _currentUser;

    public async Task<AuthenticationResult> LoginAsync(string username, string password)
    {
        try
        {
            var user = await _authService.AuthenticateAsync(username, password);
            if (user != null)
            {
                _currentUser = user;
                return AuthenticationResult.Success(user);
            }
            return AuthenticationResult.Failure("Credenciales inválidas");
        }
        catch (Exception ex)
        {
            return AuthenticationResult.Failure($"Error de autenticación: {ex.Message}");
        }
    }

    public async Task<bool> LogoutAsync()
    {
        _currentUser = null;
        return true;
    }

    public Task<User?> GetCurrentUserAsync()
    {
        return Task.FromResult(_currentUser);
    }
}