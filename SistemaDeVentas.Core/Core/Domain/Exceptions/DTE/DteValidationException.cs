using System.Runtime.Serialization;

namespace SistemaDeVentas.Core.Domain.Exceptions.DTE;

/// <summary>
/// Excepción lanzada cuando falla la validación de un DTE.
/// </summary>
[Serializable]
public class DteValidationException : Exception
{
    /// <summary>
    /// Inicializa una nueva instancia de la clase DteValidationException.
    /// </summary>
    public DteValidationException()
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase DteValidationException con un mensaje de error especificado.
    /// </summary>
    /// <param name="message">El mensaje que describe el error.</param>
    public DteValidationException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase DteValidationException con un mensaje de error especificado
    /// y una referencia a la excepción interna que es la causa de esta excepción.
    /// </summary>
    /// <param name="message">El mensaje que describe el error.</param>
    /// <param name="innerException">La excepción que es la causa de la excepción actual.</param>
    public DteValidationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Inicializa una nueva instancia de la clase DteValidationException con información de serialización.
    /// </summary>
    /// <param name="info">El objeto que contiene la información de serialización.</param>
    /// <param name="context">La información contextual sobre el origen o destino.</param>
    protected DteValidationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}