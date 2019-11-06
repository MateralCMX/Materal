using Materal.Elasticsearch.Core;
using Nest;
using System;
using System.Collections.Generic;

namespace Demo.UI
{
    public class Product : IDocument<Guid>
    {
        public Guid ID { get; set; }
        public string GetIDString()
        {
            return ID.ToString();
        }
        /// <summary>
        /// 名称
        /// </summary>
        [Text(Analyzer = "ik_max_word", SearchAnalyzer = "ik_smart")]
        public string Name { get; set; }
        /// <summary>
        /// 价格
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        [Text(Fielddata = true, Analyzer = "ik_max_word", SearchAnalyzer = "ik_smart")]
        public List<string> Tags { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Text(Analyzer = "ik_max_word", SearchAnalyzer = "ik_smart")]
        public string Desc { get; set; }
        /// <summary>
        /// 制造商
        /// </summary>
        [Text(Analyzer = "ik_max_word", SearchAnalyzer = "ik_smart")]
        public string Producer { get; set; }
    }
}
