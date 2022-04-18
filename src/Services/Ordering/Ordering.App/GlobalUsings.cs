global using MediatR;
global using Confluent.Kafka;
global using FluentValidation;
global using FluentValidation.AspNetCore;
<<<<<<< HEAD
=======
global using ECom.BuildingBlocks.SharedKernel.Interfaces;
global using ECom.Services.Ordering.Domain.AggregateModels.OrderAggregate.Rings;
>>>>>>> bcad93d (change customer to balance service + validator behavior)
global using Disruptor;
global using Disruptor.Dsl;
global using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
global using Microsoft.Extensions.Options;
global using NetMQ;
global using NetMQ.Sockets;
global using System.Text.Json;
global using System.Text.RegularExpressions;
global using FPTS.FIT.BDRD.BuildingBlocks.EventBus;
global using FPTS.FIT.BDRD.BuildingBlocks.EventBus.Kafka;
global using FPTS.FIT.BDRD.BuildingBlocks.EventBus.Core;
global using FPTS.FIT.BDRD.BuildingBlocks.EventBus.Kafka.Configurations;
global using FPTS.FIT.BDRD.BuildingBlocks.SharedKernel.Interfaces;
global using FPTS.FIT.BDRD.BuildingBlocks.SharedKernel.Extensions;
global using FPTS.FIT.BDRD.BuildingBlocks.Logger.Kafka;
global using FPTS.FIT.BDRD.BuildingBlocks.Logger.Kafka.Configs;
global using FPTS.FIT.BDRD.Services.Ordering.Domain.Exceptions;
global using FPTS.FIT.BDRD.Services.Ordering.Domain.AggregateModels.OrderAggregate;
global using FPTS.FIT.BDRD.Services.Ordering.Domain.AggregateModels.OrderAggregate.RingEvents;
global using FPTS.FIT.BDRD.Services.Ordering.Domain.AggregateModels.OrderAggregate.DomainEvents;
global using FPTS.FIT.BDRD.Services.Ordering.App.Application.Commands;
global using FPTS.FIT.BDRD.Services.Ordering.App.DTOs;
global using FPTS.FIT.BDRD.Services.Ordering.App.Application.IntegrationEvents;
global using FPTS.FIT.BDRD.Services.Ordering.App.Application.Validations;
global using FPTS.FIT.BDRD.Services.Ordering.App.Application.Behaviors;
global using FPTS.FIT.BDRD.Services.Ordering.App.BackgroundTasks;
global using FPTS.FIT.BDRD.Services.Ordering.App.Application.Queries;
global using FPTS.FIT.BDRD.Services.Ordering.Infrastructure;
=======
global using ECom.Services.Ordering.App.Application.Commands;
global using ECom.Services.Ordering.App.DTOs;
global using ECom.Services.Ordering.Domain.AggregateModels.OrderAggregate;
global using ECom.BuildingBlocks.MessageQueue.KafkaMessageQueue;
global using Confluent.Kafka;
global using ECom.Services.Ordering.App.Application.Integrations;
global using ECom.Services.Ordering.Domain.Exceptions;
global using ECom.Services.Ordering.App.Application.Validations;
global using ECom.Services.Ordering.App.Application.Behaviors;
>>>>>>> bcad93d (change customer to balance service + validator behavior)
