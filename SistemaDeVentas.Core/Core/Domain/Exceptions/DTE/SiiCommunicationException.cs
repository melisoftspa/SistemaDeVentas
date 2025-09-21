using System.Runtime.Serialization;

namespace SistemaDeVentas.Core.Domain.Exceptions.DTE;

/// <summary>
/// Excepción lanzada cuando falla la comunicación con el SII.
/// </summary>
[Serializable]
public class SiiCommunicationException : Exception
{
    /// <summary>
    /// Código de error del SII.
    /// </summary>
    public string? ErrorCode { get; }

    /// <summary>
    /// Inicializa una nueva instancia de la clase SiiCommunicationException.
    /// </summary>
    public SiiCommunicationException()
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase SiiCommunicationException con un mensaje de error especificado.
    /// </summary>
    /// <param name="message">El mensaje que describe el error.</param>
    public SiiCommunicationException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase SiiCommunicationException con un mensaje de error especificado
    /// y un código de error.
    /// </summary>
    /// <param name="message">El mensaje que describe el error.</param>
    /// <param name="errorCode">El código de error del SII.</param>
    public SiiCommunicationException(string message, string errorCode)
        : base(message)
    {
        ErrorCode = errorCode;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase SiiCommunicationException con un mensaje de error especificado
    /// y una referencia a la excepción interna que es la causa de esta excepción.
    /// </summary>
    /// <param name="message">El mensaje que describe el error.</param>
    /// <param name="innerException">La excepción que es la causa de la excepción actual.</param>
    public SiiCommunicationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase SiiCommunicationException con un mensaje de error especificado,
    /// un código de error y una referencia a la excepción interna.
    /// </summary>
    /// <param name="message">El mensaje que describe el error.</param>
    /// <param name="errorCode">El código de error del SII.</param>
    /// <param name="innerException">La excepción que es la causa de la excepción actual.</param>
    public SiiCommunicationException(string message, string errorCode, Exception innerException)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase SiiCommunicationException con información de serialización.
    /// </summary>
    /// <param name="info">El objeto que contiene la información de serialización.</param>
    /// <param name="context">La información contextual sobre el origen o destino.</param>
    protected SiiCommunicationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}