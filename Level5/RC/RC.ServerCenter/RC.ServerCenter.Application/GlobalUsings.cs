global using AutoMapper;
global using Materal.MergeBlock.Abstractions;
global using Materal.MergeBlock.Abstractions.Models;
global using Materal.MergeBlock.Application.Controllers;
global using Materal.MergeBlock.Application.Services;
global using Materal.MergeBlock.Domain;
global using Materal.TTA.Common;
global using Materal.TTA.EFRepository;
global using Materal.Utils.Model;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.DependencyInjection;
global using System.Linq.Expressions;
global using RC.Core.Abstractions;
global using RC.ServerCenter.Abstractions.Controllers;
global using RC.ServerCenter.Abstractions.Domain;
global using RC.ServerCenter.Abstractions.Repositories;
global using RC.ServerCenter.Abstractions.Services;

[assembly: MergeBlockAssembly("RCServerCenter模块", ["Authorization", "EventBus", "RC.ServerCenter.Repository", "RC.ServerCenter.Abstractions"])]