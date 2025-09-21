using System;
using System.Threading.Tasks;
using CoreInterfaces = SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.Application.Interfaces;

namespace SistemaDeVentas.WinUI.Services;

public class AuthenticationService : CoreInterfaces.IAuthenticationService
{
    private readonly IAuthService _authService;
    private User? _currentUser;

    public AuthenticationService(IAuthService authService)
    {
        _authService = authService ?? throw new ArgumentNullException(nameof(authService));
    }

    public bool IsAuthenticated => _currentUser != null;
    public User? CurrentUser => _currentUser;

    public async Task<CoreInterfaces.AuthenticationResult> LoginAsync(string username, string password)
    {
        try
        {
            var user = await _authService.AuthenticateAsync(username, password);
            if (user != null)
            {
                _currentUser = user;
                return CoreInterfaces.AuthenticationResult.Success(user);
            }
            return CoreInterfaces.AuthenticationResult.Failure("Credenciales inválidas");
        }
        catch (Exception ex)
        {
            return CoreInterfaces.AuthenticationResult.Failure($"Error de autenticación: {ex.Message}");
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