using Materal.BaseCore.CodeGenerator;
using Materal.BaseCore.CodeGenerator.Extensions;
using Materal.BaseCore.CodeGenerator.Models;
using System;
using System.Text;

namespace TestPlug
{
    public class MyPlug : IMateralBaseCoreCodeGeneratorPlug
    {
        public void PlugExecute(DomainPlugModel model)
        {
            if (model.DomainProject == null) return;
            if (model.Domain == null) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"namespace {model.Domain.Namespace}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    public class MyPlug{model.Domain.Name}ShowName");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        //{AppDomain.CurrentDomain.BaseDirectory}");
            codeContent.AppendLine($"        public void ShowName()");
            codeContent.AppendLine($"        {{");
            codeContent.AppendLine($"            Console.WriteLine(nameof({model.Domain.Name}));");
            codeContent.AppendLine($"        }}");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveFile(model.DomainProject.GeneratorRootPath, $"MyPlug{model.Domain.Name}ShowName.g.cs");
        }
    }
}