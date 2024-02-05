using Materal.MergeBlock.Abstractions;

[assembly: MergeBlockAssembly(true)]
namespace Materal.MergeBlock.OscillatorTest
{
    /// <summary>
    /// Oscillator模块
    /// </summary>
    public class OscillatorTestModule : MergeBlockModule, IMergeBlockModule
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public OscillatorTestModule() : base("Oscillator测试模块", "OscillatorTest", ["Oscillator"])
        {

        }
    }
}
