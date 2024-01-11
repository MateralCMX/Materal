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
global using RC.Authority.Abstractions.Controllers;
global using RC.Authority.Abstractions.Domain;
global using RC.Authority.Abstractions.Repositories;
global using RC.Authority.Abstractions.Services;

[assembly: MergeBlockAssembly("RCAuthority模块", ["Authorization", "RC.Authority.Repository", "RC.Authority.Abstractions"])]