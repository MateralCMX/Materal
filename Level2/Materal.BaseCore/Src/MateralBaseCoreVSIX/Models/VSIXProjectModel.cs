using EnvDTE;
using Materal.BaseCore.CodeGenerator.Models;
using Microsoft.VisualStudio.Shell;
using System.IO;

namespace MateralBaseCoreVSIX.Models
{
    public class VSIXProjectModel : ProjectModel
    {
        public VSIXProjectModel(Project project)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            RootPath = Path.GetDirectoryName(project.FullName);
            int biasIndex = RootPath.LastIndexOf('\\');
            DiskDirectoryPath = RootPath.Substring(0, biasIndex);
            GeneratorRootPath = Path.Combine(RootPath, "MCG");
            Namespace = project.Name;
            string[] names = project.Name.Split('.');
            if (names.Length >= 2)
            {
                ProjectName = names[1];
                PrefixName = names[0];
            }
            else
            {
                ProjectName = Namespace;
                PrefixName = ProjectName;
            }
            DBContextName = $"{ProjectName}DBContext";
        }
    }
}
