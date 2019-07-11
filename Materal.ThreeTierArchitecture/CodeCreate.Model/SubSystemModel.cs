using CodeCreate.Common;
using Materal.FileHelper;
using System;
using System.Collections.Generic;
using System.IO;

namespace CodeCreate.Model
{
    public sealed class SubSystemModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 拥有EF仓储实现
        /// </summary>
        public bool HasEFRepository { get; set; }
        /// <summary>
        /// 领域模型
        /// </summary>
        public List<DomainModel> Domains { get; set; }
        /// <summary>
        /// 视图模型
        /// </summary>
        public List<ViewModel> Views { get; set; }
        /// <summary>
        /// Nuget包版本
        /// </summary>
        public Dictionary<string,string> NugetPackVersion { get; set; }
        /// <summary>
        /// 创建Common文件
        /// </summary>
        /// <param name="targetPath">目标路径</param>
        private void CreateCommonFile(string targetPath)
        {
            const string name = "Common";
            string commonTargetPath = $"{targetPath}/{Name}.{name}";
            DirectoryInfo directoryInfo = Directory.Exists(commonTargetPath) ? new DirectoryInfo(commonTargetPath) : Directory.CreateDirectory(commonTargetPath);
            directoryInfo.Clear();
            var csprojModel = new CsprojModel
            {
                Name = $"{Name}.{name}",
                Sdk = "Microsoft.NET.Sdk",
                TargetFramework = "netstandard2.0"
            };
            csprojModel.CreateFile(commonTargetPath);
        }
        /// <summary>
        /// 创建Domain文件
        /// </summary>
        /// <param name="targetPath"></param>
        private void CreateDomainFile(string targetPath)
        {
            const string name = "Domain";
            string domainTargetPath = $"{targetPath}/{Name}.{name}";
            DirectoryInfo directoryInfo = Directory.Exists(domainTargetPath) ? new DirectoryInfo(domainTargetPath) : Directory.CreateDirectory(domainTargetPath);
            directoryInfo.Clear();
            var csprojModel = new CsprojModel
            {
                Name = $"{Name}.{name}",
                ItemGroups = new[]
                {
                    new[]{
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.ProjectReference,
                            Value = @"..\Domain\Domain.csproj"
                        }
                    },
                    new[]
                    {
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.Folder,
                            Value = @"Repositories\"
                        }
                    },
                    new[]
                    {
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.PackageReference,
                            Value = "Materal.TTA.Common",
                            Version = NugetPackVersion["Materal.TTA"]
                        }
                    }
                }
            };
            csprojModel.CreateFile(domainTargetPath);
            if (Domains != null)
            {
                foreach (DomainModel domain in Domains)
                {
                    domain.CreateFile(domainTargetPath, Name);
                }
            }
            CreateViewFile(domainTargetPath);
        }
        /// <summary>
        /// 创建View文件
        /// </summary>
        /// <param name="targetPath"></param>
        private void CreateViewFile(string targetPath)
        {
            const string name = "Views";
            if (Views == null) return;
            foreach (ViewModel view in Views)
            {
                view.CreateFile(targetPath, name, Name);
            }
        }
        /// <summary>
        /// 创建EF仓储
        /// </summary>
        /// <param name="targetPath"></param>
        private void CreateEFRepository(string targetPath)
        {
            const string name = "EFRepository";
            string efRepositoryTargetPath = $"{targetPath}/{Name}.{name}";
            DirectoryInfo directoryInfo = Directory.Exists(efRepositoryTargetPath) ? new DirectoryInfo(efRepositoryTargetPath) : Directory.CreateDirectory(efRepositoryTargetPath);
            directoryInfo.Clear();
            var csprojModel = new CsprojModel
            {
                Name = $"{Name}.{name}",
                ItemGroups = new[]
                {
                    new[]{
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.PackageReference,
                            Value = "Materal.TTA.SqlServerRepository",
                            Version = NugetPackVersion["Materal.TTA"]
                        },
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.PackageReference,
                            Value = "Microsoft.EntityFrameworkCore.SqlServer",
                            Version = NugetPackVersion["Microsoft.EntityFrameworkCore.SqlServer"]
                        }
                    },
                    new[]
                    {
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.ProjectReference,
                            Value = $"..\\{Name}.Domain\\{Name}.Domain.csproj"
                        }
                    },
                    new[]
                    {
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.Folder,
                            Value = @"Migrations\"
                        },
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.Folder,
                            Value = @"ModelConfig\"
                        },
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.Folder,
                            Value = @"RepositoryImpl\"
                        }
                    }
                }
            };
            csprojModel.CreateFile(efRepositoryTargetPath);
            var efRepository = new EFRepositoryModel(Domains, Views);
            efRepository.CreateFile(efRepositoryTargetPath, Name);
        }
        /// <summary>
        /// 创建数据传输模型
        /// </summary>
        /// <param name="targetPath"></param>
        private void CreateDataTransmitModel(string targetPath)
        {
            const string name = "DataTransmitModel";
            string dataTransmitModelTargetPath = $"{targetPath}/{Name}.{name}";
            DirectoryInfo directoryInfo = Directory.Exists(dataTransmitModelTargetPath) ? new DirectoryInfo(dataTransmitModelTargetPath) : Directory.CreateDirectory(dataTransmitModelTargetPath);
            directoryInfo.Clear();
            var domainFolders = new List<ItemGroupModel>();
            foreach (DomainModel domain in Domains)
            {
                if (!domain.HasService) continue;
                domainFolders.Add(new ItemGroupModel
                {
                    Type = ItemGroupType.Folder,
                    Value = domain.Name
                });
            }
            var csprojModel = new CsprojModel
            {
                Name = $"{Name}.{name}",
                HasXmlFile = true,
                ItemGroups = new[]
                {
                    new[]
                    {
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.ProjectReference,
                            Value = "..\\Common\\Common.csproj"
                        },
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.ProjectReference,
                            Value = $"..\\{Name}.Common\\{Name}.Common.csproj"
                        },
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.ProjectReference,
                            Value = $"..\\{Name}.Domain\\{Name}.Domain.csproj"
                        }
                    },
                    domainFolders.ToArray()
                }
            };
            csprojModel.CreateFile(dataTransmitModelTargetPath);
            var dataTransmitModel = new DataTransmitModel(Domains);
            dataTransmitModel.CreateFile(dataTransmitModelTargetPath, Name);
        }
        /// <summary>
        /// 创建服务
        /// </summary>
        /// <param name="targetPath"></param>
        private void CreateService(string targetPath)
        {
            const string name = "Service";
            string serviceTargetPath = $"{targetPath}/{Name}.{name}";
            DirectoryInfo directoryInfo = Directory.Exists(serviceTargetPath) ? new DirectoryInfo(serviceTargetPath) : Directory.CreateDirectory(serviceTargetPath);
            directoryInfo.Clear();
            var domainFolders = new List<ItemGroupModel>();
            foreach (DomainModel domain in Domains)
            {
                if (!domain.HasService) continue;
                domainFolders.Add(new ItemGroupModel
                {
                    Type = ItemGroupType.Folder,
                    Value = $"Model\\{domain.Name}"
                });
            }
            var csprojModel = new CsprojModel
            {
                Name = $"{Name}.{name}",
                ItemGroups = new[]
                {
                    new[]
                    {
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.ProjectReference,
                            Value = "..\\Common\\Common.csproj"
                        },
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.ProjectReference,
                            Value = $"..\\{Name}.Common\\{Name}.Common.csproj"
                        },
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.ProjectReference,
                            Value = $"..\\{Name}.DataTransmitModel\\{Name}.DataTransmitModel.csproj"
                        }
                    },
                    domainFolders.ToArray()
                }
            };
            csprojModel.CreateFile(serviceTargetPath);
            var serviceModel = new ServiceModel(Domains);
            serviceModel.CreateFile(serviceTargetPath, Name);
        }
        /// <summary>
        /// 创建服务实现
        /// </summary>
        /// <param name="targetPath"></param>
        private void CreateServiceImpl(string targetPath)
        {
            const string name = "ServiceImpl";
            string serviceTargetPath = $"{targetPath}/{Name}.{name}";
            DirectoryInfo directoryInfo = Directory.Exists(serviceTargetPath) ? new DirectoryInfo(serviceTargetPath) : Directory.CreateDirectory(serviceTargetPath);
            directoryInfo.Clear();
            var csprojModel = new CsprojModel
            {
                Name = $"{Name}.{name}",
                ItemGroups = new[]
                {
                    new[]
                    {
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.PackageReference,
                            Value = "AutoMapper",
                            Version = NugetPackVersion["AutoMapper"]
                        },
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.PackageReference,
                            Value = "Materal.CacheHelper",
                            Version = NugetPackVersion["Materal"]
                        },
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.PackageReference,
                            Value = "Materal.ConvertHelper",
                            Version = NugetPackVersion["Materal"]
                        },
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.PackageReference,
                            Value = "Materal.LinqHelper",
                            Version = NugetPackVersion["Materal"]
                        }
                    },
                    new[]
                    {
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.ProjectReference,
                            Value = "..\\Common\\Common.csproj"
                        },
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.ProjectReference,
                            Value = $"..\\{Name}.Common\\{Name}.Common.csproj"
                        },
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.ProjectReference,
                            Value = $"..\\{Name}.Domain\\{Name}.Domain.csproj"
                        },
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.ProjectReference,
                            Value = $"..\\{Name}.EFRepository\\{Name}.EFRepository.csproj"
                        },
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.ProjectReference,
                            Value = $"..\\{Name}.Service\\{Name}.Service.csproj"
                        },
                    },
                    new[]
                    {
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.Folder,
                            Value = @"AutoMapperProfile\"
                        }
                    }
                }
            };
            csprojModel.CreateFile(serviceTargetPath);
            var serviceModel = new ServiceImplModel(Domains);
            serviceModel.CreateFile(serviceTargetPath, Name);
        }
        /// <summary>
        /// 创建数据传输模型
        /// </summary>
        /// <param name="targetPath"></param>
        private void CreatePresentationModel(string targetPath)
        {
            const string name = "PresentationModel";
            string dataTransmitModelTargetPath = $"{targetPath}/{Name}.{name}";
            DirectoryInfo directoryInfo = Directory.Exists(dataTransmitModelTargetPath) ? new DirectoryInfo(dataTransmitModelTargetPath) : Directory.CreateDirectory(dataTransmitModelTargetPath);
            directoryInfo.Clear();
            var domainFolders = new List<ItemGroupModel>
            {
                new ItemGroupModel
                {
                    Type = ItemGroupType.Folder,
                    Value = "AutoMapperProfile"
                }
            };
            foreach (DomainModel domain in Domains)
            {
                if (!domain.HasService) continue;
                domainFolders.Add(new ItemGroupModel
                {
                    Type = ItemGroupType.Folder,
                    Value = $"{domain.Name}\\Request"
                });
            }
            var csprojModel = new CsprojModel
            {
                Name = $"{Name}.{name}",
                HasXmlFile = true,
                ItemGroups = new[]
                {
                    new[]
                    {
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.PackageReference,
                            Value = "AutoMapper",
                            Version = NugetPackVersion["AutoMapper"]
                        },
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.PackageReference,
                            Value = "Materal.Common",
                            Version = NugetPackVersion["Materal"]
                        },
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.PackageReference,
                            Value = "Microsoft.AspNetCore.Mvc.Core",
                            Version = NugetPackVersion["Microsoft.AspNetCore.Mvc.Core"]
                        },
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.PackageReference,
                            Value = "System.ComponentModel.Annotations",
                            Version = NugetPackVersion["System.ComponentModel.Annotations"]
                        }
                    },
                    new[]
                    {
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.ProjectReference,
                            Value = "..\\Common\\Common.csproj"
                        },
                        new ItemGroupModel
                        {
                            Type = ItemGroupType.ProjectReference,
                            Value = $"..\\{Name}.Service\\{Name}.Service.csproj"
                        }
                    },
                    domainFolders.ToArray()
                }
            };
            csprojModel.CreateFile(dataTransmitModelTargetPath);
            var presentationModel = new PresentationModel(Domains);
            presentationModel.CreateFile(dataTransmitModelTargetPath, Name);
        }
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="targetPath">目标路径</param>
        public void CreateFile(string targetPath)
        {
            if (string.IsNullOrEmpty(Name)) throw new InvalidOperationException("名称不能为空");
            CreateCommonFile(targetPath);
            CreateDomainFile(targetPath);
            if (HasEFRepository)
            {
                CreateEFRepository(targetPath);
            }
            CreateDataTransmitModel(targetPath);
            CreateService(targetPath);
            CreateServiceImpl(targetPath);
            CreatePresentationModel(targetPath);
        }
    }
}
