using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;

namespace Materal.Elasticsearch.Core
{
    public class ElasticsearchHelper<T, TIdentifier>
        where T : class, IDocument<TIdentifier>, new()
        where TIdentifier : struct
    {
        private readonly string _host;
        private ElasticClient _client;

        public ElasticsearchHelper(string host)
        {
            _host = host;
            InitClient();
            Task task = Task.Run(async () => await InitIndexAsync());
            Task.WaitAll(task);
        }
        /// <summary>
        /// 获得新的查询对象
        /// </summary>
        /// <returns></returns>
        public SearchDescriptor<T> GetNewQuery()
        {
            return new SearchDescriptor<T>();
        }
        /// <summary>
        /// 查询文档
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<List<T>> SearchDocumentAsync(SearchDescriptor<T> query)
        {
            ISearchResponse<T> searchResponse = await _client.SearchAsync<T>(query);
            List<T> documents = searchResponse.Documents.ToList();
            return documents;
        }
        /// <summary>
        /// 添加文档
        /// </summary>
        /// <param name="document">文档</param>
        /// <returns></returns>
        public async Task InsertDocumentAsync(T document)
        {
            await _client.IndexDocumentAsync(document);
        }
        /// <summary>
        /// 删除文档
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteDocumentAsync(TIdentifier id)
        {
            await _client.DeleteAsync<T>(new T
            {
                ID = id
            });
        }
        /// <summary>
        /// 修改文档
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public async Task UpdateDocumentAsync(T document)
        {
            await _client.IndexDocumentAsync(document);
        }

        #region 私有方法
        /// <summary>
        /// 获得索引名称
        /// </summary>
        /// <returns></returns>
        private string GetIndexName()
        {
            Type tType = typeof(T);
            return tType.Name.ToLower();
        }
        /// <summary>
        /// 初始化客户端
        /// </summary>
        private void InitClient()
        {
            var host = new Uri(_host);
            var settings = new ConnectionSettings(host);
            settings = settings.DefaultIndex(GetIndexName());
            settings.DefaultMappingFor<T>(m => m.IdProperty(p => p.ID));
            _client = new ElasticClient(settings);
        }
        /// <summary>
        /// 初始化索引
        /// </summary>
        /// <returns></returns>
        private async Task InitIndexAsync()
        {
            string indexName = GetIndexName();
            CatResponse<CatIndicesRecord> catResponse = await _client.Cat.IndicesAsync(i => i.AllIndices());
            if (catResponse.Records.Any(catResponseRecord => indexName == catResponseRecord.Index)) return;
            await _client.Indices.CreateAsync(indexName, c => c.Map<T>(m => m.AutoMap()));
        }
        #endregion
    }
}
