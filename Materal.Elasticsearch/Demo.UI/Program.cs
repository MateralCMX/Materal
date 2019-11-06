using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Materal.Elasticsearch.Core;
using Nest;

namespace Demo.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Task task = Task.Run(Run);
            Task.WaitAll(task);
        }

        public static async Task Run()
        {
            var product1 = new Product
            {
                ID = Guid.NewGuid(),
                Name = "高露洁牙膏",
                Price = 20,
                Desc = "一种牙膏",
                Producer = "高露洁",
                Tags = new List<string>
                {
                    "高效美白",
                    "修复牙齿"
                }
            };
            var product2 = new Product
            {
                ID = Guid.NewGuid(),
                Name = "佳洁士牙膏",
                Price = 30,
                Desc = "一种",
                Producer = "佳洁士",
                Tags = new List<string>
                {
                    "高效美白"
                }
            };
            var product3 = new Product
            {
                ID = Guid.NewGuid(),
                Name = "冷酸灵牙膏",
                Price = 40,
                Desc = "一种牙膏",
                Producer = "冷酸灵",
                Tags = new List<string>
                {
                    "修复牙齿"
                }
            };
            var elasticsearchHelper = new ElasticsearchHelper<Product, Guid>("http://116.55.251.31:9200");
            await elasticsearchHelper.InsertDocumentAsync(product1);
            await elasticsearchHelper.InsertDocumentAsync(product2);
            await elasticsearchHelper.InsertDocumentAsync(product3);
            SearchDescriptor<Product> query = elasticsearchHelper.GetNewQuery();
            query = query.From(0).Size(10).Query(q => q.Match(m => m.Field(f => f.Name).Query("牙膏")));
            await Task.Delay(2000);
            await Search(elasticsearchHelper, query);
            product1.Name = product1.Name + "1";
            product2.Name = product2.Name + "1";
            product3.Name = product3.Name + "1";
            await elasticsearchHelper.UpdateDocumentAsync(product1);
            await elasticsearchHelper.UpdateDocumentAsync(product2);
            await elasticsearchHelper.UpdateDocumentAsync(product3);
            await Task.Delay(2000);
            await Search(elasticsearchHelper, query);
            await elasticsearchHelper.DeleteDocumentAsync(product1.ID);
            await elasticsearchHelper.DeleteDocumentAsync(product2.ID);
            await elasticsearchHelper.DeleteDocumentAsync(product3.ID);
            await Task.Delay(2000);
            await Search(elasticsearchHelper, query);
        }
        private static async Task Search(ElasticsearchHelper<Product, Guid> elasticsearchHelper, SearchDescriptor<Product> query)
        {
            List<Product> result = await elasticsearchHelper.SearchDocumentAsync(query);
            foreach (Product product in result)
            {
                Console.WriteLine(product.Name);
            }
        }
    }
}
