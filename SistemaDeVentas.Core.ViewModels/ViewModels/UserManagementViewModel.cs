using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Domain.Entities;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace SistemaDeVentas.Core.ViewModels.ViewModels
{
    public class UserManagementViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        // Collections
        public ObservableCollection<User> Users { get; } = new ObservableCollection<User>();
        public ObservableCollection<string> AvailableRoles { get; } = new ObservableCollection<string>(UserRoles.AllRoles);

        // Selected user for editing
        private User? _selectedUser;
        public User? SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged();
                UpdateSelectedUserProperties();
            }
        }

        // User creation/editing properties
        private string _username = string.Empty;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged();
                ValidateUsername();
            }
        }

        private string _email = string.Empty;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
                ValidateEmail();
            }
        }

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
                ValidateName();
            }
        }

        private string _selectedRole = UserRoles.Employee;
        public string SelectedRole
        {
            get => _selectedRole;
            set
            {
                _selectedRole = value;
                OnPropertyChanged();
            }
        }

        private bool _inInvoice;
        public bool InInvoice
        {
            get => _inInvoice;
            set
            {
                _inInvoice = value;
                OnPropertyChanged();
            }
        }

        // Password change properties
        private string _currentPassword = string.Empty;
        public string CurrentPassword
        {
            get => _currentPassword;
            set
            {
                _currentPassword = value;
                OnPropertyChanged();
            }
        }

        private string _newPassword = string.Empty;
        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChanged();
                ValidateNewPassword();
            }
        }

        private string _confirmPassword = string.Empty;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
                ValidateConfirmPassword();
            }
        }

        // Search and filter
        private string _searchTerm = string.Empty;
        public string SearchTerm
        {
            get => _searchTerm;
            set
            {
                _searchTerm = value;
                OnPropertyChanged();
                FilterUsers();
            }
        }

        // Validation properties
        private string _usernameError = string.Empty;
        public string UsernameError
        {
            get => _usernameError;
            set
            {
                _usernameError = value;
                OnPropertyChanged();
            }
        }

        private string _successMessage = string.Empty;
        public string SuccessMessage
        {
            get => _successMessage;
            set
            {
                _successMessage = value;
                OnPropertyChanged();
            }
        }

        private string _emailError = string.Empty;
        public string EmailError
        {
            get => _emailError;
            set
            {
                _emailError = value;
                OnPropertyChanged();
            }
        }

        private string _nameError = string.Empty;
        public string NameError
        {
            get => _nameError;
            set
            {
                _nameError = value;
                OnPropertyChanged();
            }
        }

        private string _passwordError = string.Empty;
        public string PasswordError
        {
            get => _passwordError;
            set
            {
                _passwordError = value;
                OnPropertyChanged();
            }
        }

        // Commands
        public ICommand LoadUsersCommand { get; }
        public ICommand CreateUserCommand { get; }
        public ICommand UpdateUserCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand ChangePasswordCommand { get; }
        public ICommand ActivateUserCommand { get; }
        public ICommand DeactivateUserCommand { get; }
        public ICommand ClearFormCommand { get; }
        public ICommand RefreshCommand { get; }

        public UserManagementViewModel(IUserService userService)
        {
            _userService = userService;

            LoadUsersCommand = new RelayCommand(async () => await LoadUsersAsync());
            CreateUserCommand = new RelayCommand(async () => await CreateUserAsync(), CanCreateUser);
            UpdateUserCommand = new RelayCommand(async () => await UpdateUserAsync(), CanUpdateUser);
            DeleteUserCommand = new RelayCommand(async () => await DeleteUserAsync(), CanDeleteUser);
            ChangePasswordCommand = new RelayCommand(async () => await ChangePasswordAsync(), CanChangePassword);
            ActivateUserCommand = new RelayCommand(async () => await ActivateUserAsync(), CanActivateUser);
            DeactivateUserCommand = new RelayCommand(async () => await DeactivateUserAsync(), CanDeactivateUser);
            ClearFormCommand = new RelayCommand(async () => ClearForm());
            RefreshCommand = new RelayCommand(async () => await LoadUsersAsync());

            // Load users on initialization
            LoadUsersCommand.Execute(null);
        }

        private async Task LoadUsersAsync()
        {
            try
            {
                IsBusy = true;
                var users = await _userService.GetAllUsersAsync();
                Users.Clear();
                foreach (var user in users.OrderBy(u => u.Name))
                {
                    Users.Add(user);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al cargar usuarios: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task CreateUserAsync()
        {
            try
            {
                IsBusy = true;

                var newUser = new User
                {
                    Username = Username.Trim(),
                    Email = Email.Trim(),
                    Name = Name.Trim(),
                    Role = SelectedRole,
                    Password = NewPassword,
                    InInvoice = InInvoice,
                    IsActive = true
                };

                var createdUser = await _userService.CreateUserAsync(newUser);
                Users.Add(createdUser);
                ClearForm();
                SuccessMessage = "Usuario creado exitosamente";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al crear usuario: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task UpdateUserAsync()
        {
            if (SelectedUser == null) return;

            try
            {
                IsBusy = true;

                SelectedUser.Name = Name.Trim();
                SelectedUser.Email = Email.Trim();
                SelectedUser.Role = SelectedRole;
                SelectedUser.InInvoice = InInvoice;

                var updatedUser = await _userService.UpdateUserAsync(SelectedUser);

                // Update in collection
                var index = Users.IndexOf(SelectedUser);
                if (index >= 0)
                {
                    Users[index] = updatedUser;
                }

                SuccessMessage = "Usuario actualizado exitosamente";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al actualizar usuario: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task DeleteUserAsync()
        {
            if (SelectedUser == null) return;

            try
            {
                IsBusy = true;

                var result = await _userService.DeleteUserAsync(SelectedUser.Id);
                if (result)
                {
                    Users.Remove(SelectedUser);
                    ClearForm();
                    SuccessMessage = "Usuario eliminado exitosamente";
                }
                else
                {
                    ErrorMessage = "No se pudo eliminar el usuario";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al eliminar usuario: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ChangePasswordAsync()
        {
            if (SelectedUser == null) return;

            try
            {
                IsBusy = true;

                var result = await _userService.ChangePasswordAsync(SelectedUser.Id, CurrentPassword, NewPassword);
                if (result)
                {
                    ClearPasswordFields();
                    SuccessMessage = "Contraseña cambiada exitosamente";
                }
                else
                {
                    ErrorMessage = "No se pudo cambiar la contraseña. Verifique la contraseña actual.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al cambiar contraseña: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ActivateUserAsync()
        {
            if (SelectedUser == null) return;

            try
            {
                IsBusy = true;

                var result = await _userService.ActivateUserAsync(SelectedUser.Id);
                if (result)
                {
                    SelectedUser.IsActive = true;
                    OnPropertyChanged(nameof(SelectedUser));
                    SuccessMessage = "Usuario activado exitosamente";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al activar usuario: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task DeactivateUserAsync()
        {
            if (SelectedUser == null) return;

            try
            {
                IsBusy = true;

                var result = await _userService.DeactivateUserAsync(SelectedUser.Id);
                if (result)
                {
                    SelectedUser.IsActive = false;
                    OnPropertyChanged(nameof(SelectedUser));
                    SuccessMessage = "Usuario desactivado exitosamente";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al desactivar usuario: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void UpdateSelectedUserProperties()
        {
            if (SelectedUser != null)
            {
                Username = SelectedUser.Username;
                Email = SelectedUser.Email;
                Name = SelectedUser.Name;
                SelectedRole = SelectedUser.Role;
                InInvoice = SelectedUser.InInvoice;
            }
        }

        private void ClearForm()
        {
            SelectedUser = null;
            Username = string.Empty;
            Email = string.Empty;
            Name = string.Empty;
            SelectedRole = UserRoles.Employee;
            InInvoice = false;
            ClearPasswordFields();
            ClearValidationErrors();
            SuccessMessage = string.Empty;
            ErrorMessage = string.Empty;
        }

        private void ClearPasswordFields()
        {
            CurrentPassword = string.Empty;
            NewPassword = string.Empty;
            ConfirmPassword = string.Empty;
        }

        private void ClearValidationErrors()
        {
            UsernameError = string.Empty;
            EmailError = string.Empty;
            NameError = string.Empty;
            PasswordError = string.Empty;
        }

        private async void FilterUsers()
        {
            if (string.IsNullOrWhiteSpace(SearchTerm))
            {
                await LoadUsersAsync();
                return;
            }

            try
            {
                var filteredUsers = await _userService.SearchUsersAsync(SearchTerm);
                Users.Clear();
                foreach (var user in filteredUsers.OrderBy(u => u.Name))
                {
                    Users.Add(user);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al filtrar usuarios: {ex.Message}";
            }
        }

        // Validation methods
        private void ValidateUsername()
        {
            UsernameError = string.IsNullOrWhiteSpace(Username) ? "El nombre de usuario es requerido" :
                           Username.Length < 3 ? "El nombre de usuario debe tener al menos 3 caracteres" :
                           Username.Length > 50 ? "El nombre de usuario no puede exceder 50 caracteres" : string.Empty;
        }

        private void ValidateEmail()
        {
            EmailError = string.IsNullOrWhiteSpace(Email) ? "El email es requerido" :
                        !IsValidEmail(Email) ? "El email no tiene un formato válido" : string.Empty;
        }

        private void ValidateName()
        {
            NameError = string.IsNullOrWhiteSpace(Name) ? "El nombre es requerido" :
                       Name.Length < 2 ? "El nombre debe tener al menos 2 caracteres" :
                       Name.Length > 100 ? "El nombre no puede exceder 100 caracteres" : string.Empty;
        }

        private void ValidateNewPassword()
        {
            PasswordError = string.IsNullOrWhiteSpace(NewPassword) ? "La nueva contraseña es requerida" :
                           NewPassword.Length < 6 ? "La contraseña debe tener al menos 6 caracteres" : string.Empty;
        }

        private void ValidateConfirmPassword()
        {
            if (!string.IsNullOrWhiteSpace(ConfirmPassword) && NewPassword != ConfirmPassword)
            {
                PasswordError = "Las contraseñas no coinciden";
            }
        }

        // Command can execute methods
        private bool CanCreateUser() =>
            !string.IsNullOrWhiteSpace(Username) &&
            !string.IsNullOrWhiteSpace(Email) &&
            !string.IsNullOrWhiteSpace(Name) &&
            !string.IsNullOrWhiteSpace(NewPassword) &&
            NewPassword == ConfirmPassword &&
            string.IsNullOrEmpty(UsernameError) &&
            string.IsNullOrEmpty(EmailError) &&
            string.IsNullOrEmpty(NameError) &&
            string.IsNullOrEmpty(PasswordError);

        private bool CanUpdateUser() =>
            SelectedUser != null &&
            !string.IsNullOrWhiteSpace(Name) &&
            !string.IsNullOrWhiteSpace(Email) &&
            string.IsNullOrEmpty(NameError) &&
            string.IsNullOrEmpty(EmailError);

        private bool CanDeleteUser() => SelectedUser != null;

        private bool CanChangePassword() =>
            SelectedUser != null &&
            !string.IsNullOrWhiteSpace(CurrentPassword) &&
            !string.IsNullOrWhiteSpace(NewPassword) &&
            NewPassword == ConfirmPassword &&
            string.IsNullOrEmpty(PasswordError);

        private bool CanActivateUser() => SelectedUser != null && !SelectedUser.IsActive;
        private bool CanDeactivateUser() => SelectedUser != null && SelectedUser.IsActive;

        // Helper methods
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}