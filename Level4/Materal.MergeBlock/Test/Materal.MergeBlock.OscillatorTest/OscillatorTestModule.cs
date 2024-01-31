using Materal.MergeBlock.Abstractions;

[assembly: MergeBlockAssembly]
namespace Materal.MergeBlock.OscillatorTest
{
    /// <summary>
    /// Oscillator模块
    /// </summary>
    public class OscillatorTestModule : MergeBlockModule, IMergeBlockModule
    {
        public OscillatorTestModule() : base("Oscillator测试模块", "OscillatorTest", ["Oscillator"])
        {

        }
    }
}
