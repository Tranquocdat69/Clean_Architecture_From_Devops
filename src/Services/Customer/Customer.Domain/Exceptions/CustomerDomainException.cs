namespace ECom.Services.Customer.Domain.Exceptions;
/// <summary>
/// Exception type for domain exceptions
/// </summary>
public class CustomerDomainException : Exception
{
    public CustomerDomainException()
    { }

    public CustomerDomainException(string message)
        : base(message)
    { }

    public CustomerDomainException(string message, Exception innerException)
        : base(message, innerException)
    { }
}
