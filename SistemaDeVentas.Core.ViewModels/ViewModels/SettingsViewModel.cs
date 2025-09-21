using SistemaDeVentas.Core.Application.Interfaces;
using SistemaDeVentas.Core.Domain.Entities;
using SistemaDeVentas.Core.ViewModels.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SistemaDeVentas.Core.ViewModels.ViewModels
{
    /// <summary>
    /// ViewModel para la página de configuración del sistema
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {
        private readonly IParameterService _parameterService;

        // Configuración General
        private string _companyName = string.Empty;
        private string _companyRUT = string.Empty;
        private string _companyAddress = string.Empty;
        private string _companyPhone = string.Empty;
        private string _companyEmail = string.Empty;

        // Configuración MercadoPago
        private string _mercadoPagoAccessToken = string.Empty;
        private string _mercadoPagoTerminalId = string.Empty;
        private string _mercadoPagoBaseUrl = "https://api.mercadopago.com";
        private int _mercadoPagoTimeoutSeconds = 30;
        private string _mercadoPagoDefaultCurrency = "CLP";

        // Configuración DTE/SII
        private string _siiUsername = string.Empty;
        private string _siiPassword = string.Empty;
        private string _certificatePath = string.Empty;
        private string _certificatePassword = string.Empty;
        private string _ambiente = "certificacion"; // certificacion o produccion
        private string _rutEmisor = string.Empty;
        private string _razonSocial = string.Empty;
        private string _giroEmisor = string.Empty;
        private int _actividadEconomica = 0;
        private string _direccionOrigen = string.Empty;
        private string _comunaOrigen = string.Empty;

        public SettingsViewModel(IParameterService parameterService)
        {
            _parameterService = parameterService ?? throw new ArgumentNullException(nameof(parameterService));

            Title = "Configuración del Sistema";
            LoadSettingsCommand = new RelayCommand(async () => await LoadSettingsAsync());
            SaveSettingsCommand = new RelayCommand(async () => await SaveSettingsAsync());
        }

        #region Propiedades de Configuración General

        public string CompanyName
        {
            get => _companyName;
            set => SetProperty(ref _companyName, value);
        }

        public string CompanyRUT
        {
            get => _companyRUT;
            set => SetProperty(ref _companyRUT, value);
        }

        public string CompanyAddress
        {
            get => _companyAddress;
            set => SetProperty(ref _companyAddress, value);
        }

        public string CompanyPhone
        {
            get => _companyPhone;
            set => SetProperty(ref _companyPhone, value);
        }

        public string CompanyEmail
        {
            get => _companyEmail;
            set => SetProperty(ref _companyEmail, value);
        }

        #endregion

        #region Propiedades de Configuración MercadoPago

        public string MercadoPagoAccessToken
        {
            get => _mercadoPagoAccessToken;
            set => SetProperty(ref _mercadoPagoAccessToken, value);
        }

        public string MercadoPagoTerminalId
        {
            get => _mercadoPagoTerminalId;
            set => SetProperty(ref _mercadoPagoTerminalId, value);
        }

        public string MercadoPagoBaseUrl
        {
            get => _mercadoPagoBaseUrl;
            set => SetProperty(ref _mercadoPagoBaseUrl, value);
        }

        public int MercadoPagoTimeoutSeconds
        {
            get => _mercadoPagoTimeoutSeconds;
            set => SetProperty(ref _mercadoPagoTimeoutSeconds, value);
        }

        public string MercadoPagoDefaultCurrency
        {
            get => _mercadoPagoDefaultCurrency;
            set => SetProperty(ref _mercadoPagoDefaultCurrency, value);
        }

        #endregion

        #region Propiedades de Configuración DTE/SII

        public string SiiUsername
        {
            get => _siiUsername;
            set => SetProperty(ref _siiUsername, value);
        }

        public string SiiPassword
        {
            get => _siiPassword;
            set => SetProperty(ref _siiPassword, value);
        }

        public string CertificatePath
        {
            get => _certificatePath;
            set => SetProperty(ref _certificatePath, value);
        }

        public string CertificatePassword
        {
            get => _certificatePassword;
            set => SetProperty(ref _certificatePassword, value);
        }

        public string Ambiente
        {
            get => _ambiente;
            set => SetProperty(ref _ambiente, value);
        }

        public string RutEmisor
        {
            get => _rutEmisor;
            set => SetProperty(ref _rutEmisor, value);
        }

        public string RazonSocial
        {
            get => _razonSocial;
            set => SetProperty(ref _razonSocial, value);
        }

        public string GiroEmisor
        {
            get => _giroEmisor;
            set => SetProperty(ref _giroEmisor, value);
        }

        public int ActividadEconomica
        {
            get => _actividadEconomica;
            set => SetProperty(ref _actividadEconomica, value);
        }

        public string DireccionOrigen
        {
            get => _direccionOrigen;
            set => SetProperty(ref _direccionOrigen, value);
        }

        public string ComunaOrigen
        {
            get => _comunaOrigen;
            set => SetProperty(ref _comunaOrigen, value);
        }

        #endregion

        #region Commands

        public RelayCommand LoadSettingsCommand { get; }
        public RelayCommand SaveSettingsCommand { get; }

        #endregion

        #region Métodos

        private async Task LoadSettingsAsync()
        {
            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;

                // Cargar configuración general
                CompanyName = await GetParameterValueAsync("COMPANY_NAME", "Mi Empresa S.A.");
                CompanyRUT = await GetParameterValueAsync("COMPANY_RUT", "");
                CompanyAddress = await GetParameterValueAsync("COMPANY_ADDRESS", "");
                CompanyPhone = await GetParameterValueAsync("COMPANY_PHONE", "");
                CompanyEmail = await GetParameterValueAsync("COMPANY_EMAIL", "");

                // Cargar configuración MercadoPago
                MercadoPagoAccessToken = await GetParameterValueAsync("MERCADOPAGO_ACCESS_TOKEN", "");
                MercadoPagoTerminalId = await GetParameterValueAsync("MERCADOPAGO_TERMINAL_ID", "");
                MercadoPagoBaseUrl = await GetParameterValueAsync("MERCADOPAGO_BASE_URL", "https://api.mercadopago.com");
                var timeoutStr = await GetParameterValueAsync("MERCADOPAGO_TIMEOUT_SECONDS", "30");
                MercadoPagoTimeoutSeconds = int.TryParse(timeoutStr, out var timeout) ? timeout : 30;
                MercadoPagoDefaultCurrency = await GetParameterValueAsync("MERCADOPAGO_DEFAULT_CURRENCY", "CLP");

                // Cargar configuración DTE/SII
                SiiUsername = await GetParameterValueAsync("SII_USERNAME", "");
                SiiPassword = await GetParameterValueAsync("SII_PASSWORD", "");
                CertificatePath = await GetParameterValueAsync("CERTIFICATE_PATH", "");
                CertificatePassword = await GetParameterValueAsync("CERTIFICATE_PASSWORD", "");
                Ambiente = await GetParameterValueAsync("AMBIENTE", "certificacion");
                RutEmisor = await GetParameterValueAsync("RUT_EMISOR", "");
                RazonSocial = await GetParameterValueAsync("RAZON_SOCIAL", "");
                GiroEmisor = await GetParameterValueAsync("GIRO_EMISOR", "");
                var actividadStr = await GetParameterValueAsync("ACTIVIDAD_ECONOMICA", "0");
                ActividadEconomica = int.TryParse(actividadStr, out var actividad) ? actividad : 0;
                DireccionOrigen = await GetParameterValueAsync("DIRECCION_ORIGEN", "");
                ComunaOrigen = await GetParameterValueAsync("COMUNA_ORIGEN", "");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al cargar configuración: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task SaveSettingsAsync()
        {
            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;

                // Guardar configuración general
                await SetParameterValueAsync("COMPANY_NAME", CompanyName);
                await SetParameterValueAsync("COMPANY_RUT", CompanyRUT);
                await SetParameterValueAsync("COMPANY_ADDRESS", CompanyAddress);
                await SetParameterValueAsync("COMPANY_PHONE", CompanyPhone);
                await SetParameterValueAsync("COMPANY_EMAIL", CompanyEmail);

                // Guardar configuración MercadoPago
                await SetParameterValueAsync("MERCADOPAGO_ACCESS_TOKEN", MercadoPagoAccessToken);
                await SetParameterValueAsync("MERCADOPAGO_TERMINAL_ID", MercadoPagoTerminalId);
                await SetParameterValueAsync("MERCADOPAGO_BASE_URL", MercadoPagoBaseUrl);
                await SetParameterValueAsync("MERCADOPAGO_TIMEOUT_SECONDS", MercadoPagoTimeoutSeconds.ToString());
                await SetParameterValueAsync("MERCADOPAGO_DEFAULT_CURRENCY", MercadoPagoDefaultCurrency);

                // Guardar configuración DTE/SII
                await SetParameterValueAsync("SII_USERNAME", SiiUsername);
                await SetParameterValueAsync("SII_PASSWORD", SiiPassword);
                await SetParameterValueAsync("CERTIFICATE_PATH", CertificatePath);
                await SetParameterValueAsync("CERTIFICATE_PASSWORD", CertificatePassword);
                await SetParameterValueAsync("AMBIENTE", Ambiente);
                await SetParameterValueAsync("RUT_EMISOR", RutEmisor);
                await SetParameterValueAsync("RAZON_SOCIAL", RazonSocial);
                await SetParameterValueAsync("GIRO_EMISOR", GiroEmisor);
                await SetParameterValueAsync("ACTIVIDAD_ECONOMICA", ActividadEconomica.ToString());
                await SetParameterValueAsync("DIRECCION_ORIGEN", DireccionOrigen);
                await SetParameterValueAsync("COMUNA_ORIGEN", ComunaOrigen);

                // Mostrar mensaje de éxito
                ErrorMessage = "Configuración guardada exitosamente";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Error al guardar configuración: {ex.Message}";
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task<string> GetParameterValueAsync(string key, string defaultValue)
        {
            try
            {
                var parameter = await _parameterService.GetByKeyAsync(key);
                return parameter?.Value ?? defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        private async Task SetParameterValueAsync(string key, string value)
        {
            var parameter = new Parameter
            {
                Name = key,
                Value = value,
                Type = "string",
                Module = GetParameterModule(key)
            };

            await _parameterService.UpsertAsync(parameter);
        }

        private string GetParameterModule(string key)
        {
            return key switch
            {
                "COMPANY_NAME" or "COMPANY_RUT" or "COMPANY_ADDRESS" or "COMPANY_PHONE" or "COMPANY_EMAIL" => "Empresa",
                "MERCADOPAGO_ACCESS_TOKEN" or "MERCADOPAGO_TERMINAL_ID" or "MERCADOPAGO_BASE_URL" or "MERCADOPAGO_TIMEOUT_SECONDS" or "MERCADOPAGO_DEFAULT_CURRENCY" => "MercadoPago",
                "SII_USERNAME" or "SII_PASSWORD" or "CERTIFICATE_PATH" or "CERTIFICATE_PASSWORD" or "AMBIENTE" or "RUT_EMISOR" or "RAZON_SOCIAL" or "GIRO_EMISOR" or "ACTIVIDAD_ECONOMICA" or "DIRECCION_ORIGEN" or "COMUNA_ORIGEN" => "DTE",
                _ => "General"
            };
        }

        #endregion
    }
}