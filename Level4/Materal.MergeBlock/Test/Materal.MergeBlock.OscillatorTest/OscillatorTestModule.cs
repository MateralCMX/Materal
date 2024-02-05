using Materal.MergeBlock.Abstractions;
using Materal.MergeBlock.Abstractions.WebModule;

[assembly: MergeBlockAssembly(true)]
namespace Materal.MergeBlock.OscillatorTest
{
    /// <summary>
    /// Oscillator模块
    /// </summary>
    public class OscillatorTestModule : MergeBlockWebModule, IMergeBlockWebModule
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public OscillatorTestModule() : base("Oscillator测试模块", "OscillatorTest", ["Oscillator"])
        {

        }
    }
}
