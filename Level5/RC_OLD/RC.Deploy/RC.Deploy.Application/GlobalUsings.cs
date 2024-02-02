global using AutoMapper;
global using Materal.Abstractions;
global using Materal.Extensions;
global using Materal.MergeBlock.Abstractions;
global using Materal.MergeBlock.Application.Controllers;
global using Materal.MergeBlock.Application.Services;
global using Materal.TTA.Common;
global using Materal.Utils.Model;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.DependencyInjection;
global using RC.Core.Abstractions;
global using RC.Deploy.Abstractions.Controllers;
global using RC.Deploy.Abstractions.Domain;
global using RC.Deploy.Abstractions.Enums;
global using RC.Deploy.Abstractions.Repositories;
global using RC.Deploy.Abstractions.Services;
global using System.Linq.Expressions;

[assembly: MergeBlockAssembly("RCDeploy模块", ["Authorization", "RC.Deploy.Repository", "RC.Deploy.Abstractions"])]