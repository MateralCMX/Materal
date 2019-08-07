using Demo.Common;
using Materal.Common;
using System;

namespace Demo.DataTransmitModel.Student
{
    public class StudentListDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public byte Age { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public SexEnum Sex { private get; set; }
        /// <summary>
        /// 性别字符串
        /// </summary>
        private string SexString => Sex.GetDescription();
        /// <summary>
        /// 所属班级名称
        /// </summary>
        public string BelongClassName { get; set; }
    }
}
