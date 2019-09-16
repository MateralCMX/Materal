using System;
using TTA.Core.Domain;

namespace Demo.Domain
{
    public enum HomeWorkType : byte
    {
        Math = 0,
        Physics = 1,
        Chemistry = 2,
        Biology = 3
    }
    public class HomeWork : BaseMongoEntity<Guid>
    {
        public HomeWorkType Type { get; set; }
        public string Name { get; set; }
    }
    /// <summary>
    /// 数学作业
    /// </summary>
    public class MathHomeWork : HomeWork
    {
        /// <summary>
        /// 计算结果
        /// </summary>
        public int Computation { get; set; }
    }
    /// <summary>
    /// 物理作业
    /// </summary>
    public class PhysicsHomWork : HomeWork
    {
        /// <summary>
        /// 公式
        /// </summary>
        public string Formula { get; set; }
    }
    /// <summary>
    /// 化学作业
    /// </summary>
    public class ChemistryHomWork : HomeWork
    {
        /// <summary>
        /// 元素
        /// </summary>
        public string Element { get; set; }
    }
    /// <summary>
    /// 生物作业
    /// </summary>
    public class BiologyHomWork : HomeWork
    {
        /// <summary>
        /// 细胞名称
        /// </summary>
        public string CellName { get; set; }
    }
}
