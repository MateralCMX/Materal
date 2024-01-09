global using Materal.MergeBlock.Abstractions;
global using Materal.MergeBlock.Application.Controllers;
global using Materal.Utils.Model;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.DependencyInjection;
global using MMB.Core.Abstractions;
//global using MMB.Demo.Abstractions.Controllers;
global using MMB.Demo.Abstractions.Domain;
global using MMB.Demo.Abstractions.Repositories;
global using MMB.Demo.Abstractions.Services;
global using Microsoft.Extensions.Logging;

[assembly: MergeBlockAssembly("MMBDemo模块", ["Authorzation", "MMB.Demo.Repository", "MMB.Demo.Abstractions"])]