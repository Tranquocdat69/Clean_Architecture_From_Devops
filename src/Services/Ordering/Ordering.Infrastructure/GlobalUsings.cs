global using ECom.Services.Ordering.Domain.AggregateModels.OrderAggregate;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using FPTS.FIT.BDRD.BuildingBlocks.SharedKernel.Interfaces;
global using ECom.Services.Ordering.Infrastructure.EntityConfigurations;
global using MediatR;
global using FPTS.FIT.BDRD.BuildingBlocks.SharedKernel.Extensions;
global using Microsoft.Extensions.Logging;
global using Polly.Retry;
global using Polly;

