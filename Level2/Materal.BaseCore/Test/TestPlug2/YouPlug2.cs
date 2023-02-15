﻿using Materal.BaseCore.CodeGenerator;
using Materal.BaseCore.CodeGenerator.Extensions;
using Materal.BaseCore.CodeGenerator.Models;
using System;
using System.Text;

namespace TestPlug2
{
    public class YouPlug2 : IMateralBaseCoreCodeGeneratorPlug
    {
        public void PlugExecute(DomainPlugModel model)
        {
            if (model.DomainProject == null) return;
            if (model.Domain == null) return;
            StringBuilder codeContent = new();
            codeContent.AppendLine($"namespace {model.Domain.Namespace}");
            codeContent.AppendLine($"{{");
            codeContent.AppendLine($"    public class YouPlug{model.Domain.Name}ShowName2");
            codeContent.AppendLine($"    {{");
            codeContent.AppendLine($"        //{AppDomain.CurrentDomain.BaseDirectory}");
            codeContent.AppendLine($"        public void ShowName()");
            codeContent.AppendLine($"        {{");
            codeContent.AppendLine($"            Console.WriteLine(nameof({model.Domain.Name}));");
            codeContent.AppendLine($"        }}");
            codeContent.AppendLine($"    }}");
            codeContent.AppendLine($"}}");
            codeContent.SaveFile(model.DomainProject.GeneratorRootPath, $"YouPlug{model.Domain.Name}ShowName2.g.cs");
        }
    }
}