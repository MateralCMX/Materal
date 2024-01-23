global using AutoMapper;
global using Materal.Extensions;
global using Materal.MergeBlock.Abstractions;
global using Materal.MergeBlock.Application.Controllers;
global using Materal.MergeBlock.Application.Services;
global using Materal.Utils.Model;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.DependencyInjection;
global using RC.Authority.Abstractions.Controllers;
global using RC.Authority.Abstractions.Domain;
global using RC.Authority.Abstractions.Repositories;
global using RC.Authority.Abstractions.Services;
global using RC.Core.Abstractions;

[assembly: MergeBlockAssembly("RCAuthority模块", ["Authorization", "RC.Authority.Repository", "RC.Authority.Abstractions"])]