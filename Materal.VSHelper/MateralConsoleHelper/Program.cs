using MateralVSHelper.CodeGenerator;
using System;
using System.IO;

namespace MateralConsoleHelper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string[] codes = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "User.txt"));
            for (int i = 0; i < codes.Length; i++)
            {
                string namespaceCode = codes[i];
                if (!namespaceCode.StartsWith("namespace ") || !namespaceCode.EndsWith(".Domain")) continue;
                for (int j = i; j < codes.Length; j++)
                {
                    string classCode = codes[j];
                    int publicIndex = classCode.IndexOf("public ");
                    if (publicIndex <= 0) continue;
                    int classIndex = classCode.IndexOf(" class ");
                    if (classIndex <= 0) continue;
                    int domainIndex = classCode.IndexOf(" : BaseDomain, IDomain");
                    if (domainIndex <= 0) continue;
                    var a = new DomainModel(codes, j);
                    return;
                }
            }
        }
    }
}
