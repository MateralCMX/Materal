global using Materal.MergeBlock.Abstractions;
global using Materal.MergeBlock.Application.Controllers;
global using Materal.Utils.Model;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.DependencyInjection;
global using RC.Core.Abstractions;
global using RC.Demo.Abstractions.Controllers;
global using RC.Demo.Abstractions.Domain;
global using RC.Demo.Abstractions.Repositories;
global using RC.Demo.Abstractions.Services;
global using Microsoft.Extensions.Logging;

[assembly: MergeBlockAssembly("RCDemo模块", ["Authorzation", "RC.Demo.Repository", "RC.Demo.Abstractions"])]