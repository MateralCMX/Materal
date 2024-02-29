global using AutoMapper;
global using Materal.Extensions;
global using Materal.MergeBlock.Abstractions;
global using Materal.MergeBlock.Abstractions.Models;
global using Materal.MergeBlock.Abstractions.Services;
global using Materal.MergeBlock.Abstractions.WebModule.Authorization;
global using Materal.MergeBlock.Abstractions.WebModule.Controllers;
global using Materal.MergeBlock.Abstractions.WebModule.Models;
global using Materal.MergeBlock.Application.Services;

/* 项目“RC.Demo.Application (net6.0)”的未合并的更改
在此之前:
global using Materal.MergeBlock.Abstractions.WebModule.Models;
在此之后:
global using Materal.MergeBlock.Abstractions.WebModule.Controllers;
*/
global using Materal.MergeBlock.Application.WebModule.Controllers;
global using Materal.Utils.Model;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Options;
global using RC.Core.Abstractions;
global using RC.Core.Application;
global using RC.Demo.Abstractions;
global using RC.Demo.Abstractions.Controllers;
global using RC.Demo.Abstractions.Domain;
global using RC.Demo.Abstractions.Repositories;
global using RC.Demo.Abstractions.Services;
global using RC.Demo.Application;
global using System.ComponentModel.DataAnnotations;

[assembly: MergeBlockAssembly(true)]