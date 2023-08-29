using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Materal.BaseCore.AutoDITest
{
    public static class TestHelper
    {
        public static async Task Verify(string source, IIncrementalGenerator generator)
        {
            // Parse the provided string into a C# syntax tree
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);
            IEnumerable<PortableExecutableReference> references = new[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
            };
            // Create a Roslyn compilation for the syntax tree.
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName: "Tests",
                syntaxTrees: new[] { syntaxTree },
                references: references);
            // The GeneratorDriver is used to run our generator against a compilation
            GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);
            // Run the source generator!
            driver = driver.RunGenerators(compilation);
            // Use verify to snapshot test the source generator output!
            await Verifier.Verify(driver);
        }
    }
}
