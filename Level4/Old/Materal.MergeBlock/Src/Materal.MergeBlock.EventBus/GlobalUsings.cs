global using Materal.Extensions;
global using Materal.MergeBlock.Abstractions;
global using Materal.TFMS.EventBus;
global using Materal.TFMS.EventBus.RabbitMQ;
global using Materal.TFMS.EventBus.RabbitMQ.Extensions;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using RabbitMQ.Client;

[assembly: MergeBlockAssembly("EventBus模块", "EventBus")]