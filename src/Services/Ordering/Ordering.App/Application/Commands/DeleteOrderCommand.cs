﻿namespace ECom.Services.Ordering.App.Application.Commands
{
    public record DeleteOrderCommand(string OrderId) : IRequest<bool>
    {
         
    }
}
