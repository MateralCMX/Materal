global using AutoMapper;
global using Materal.MergeBlock.Abstractions;
global using Materal.MergeBlock.Application.Controllers;
global using Materal.MergeBlock.Application.Services;
global using Materal.TTA.Common;
global using Materal.Utils.Model;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.DependencyInjection;
global using RC.Core.Abstractions;
global using RC.EnvironmentServer.Abstractions.Controllers;
global using RC.EnvironmentServer.Abstractions.Domain;
global using RC.EnvironmentServer.Abstractions.Repositories;
global using RC.EnvironmentServer.Abstractions.Services;
global using System.Linq.Expressions;

[assembly: MergeBlockAssembly("RCEnvironmentServer模块", ["Authorization", "EventBus", "RC.EnvironmentServer.Repository", "RC.EnvironmentServer.Abstractions"])]