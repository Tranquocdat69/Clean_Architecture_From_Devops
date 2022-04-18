<<<<<<< HEAD
﻿namespace FPTS.FIT.BDRD.Services.Balance.Domain.Exceptions;
=======
﻿namespace ECom.Services.Balance.Domain.Exceptions;
>>>>>>> bcad93d (change customer to balance service + validator behavior)
/// <summary>
/// Exception type for domain exceptions
/// </summary>
public class BalanceDomainException : Exception
{
    public BalanceDomainException()
    { }

    public BalanceDomainException(string message)
        : base(message)
    { }

    public BalanceDomainException(string message, Exception innerException)
        : base(message, innerException)
    { }
}
