using Materal.MergeBlock.Abstractions;
using Materal.MergeBlock.Abstractions.WebModule;

[assembly: MergeBlockAssembly(true)]
namespace Materal.MergeBlock.OscillatorTest
{
    /// <summary>
    /// Oscillator模块
    /// </summary>
    public class OscillatorTestModule() : MergeBlockWebModule("Oscillator测试模块", "OscillatorTest", ["Oscillator"])
    {
    }
}
