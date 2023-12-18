global using Materal.MergeBlock.Abstractions;
global using Materal.Utils.Model;
global using MMB.Demo.Abstractions.Enums;
global using System.ComponentModel.DataAnnotations;

[assembly: MergeBlockAssembly("MMBDemo模块", ["Authorzation", "EventBus", "MMB.Demo.Repository", "MMB.Demo.Abstractions"])]