using Nest;

namespace CrossCutting.Elasticsearch;

public interface IElasticsearchService
{
    Task IndexDocumentAsync<T>(T document, string indexName) where T : class;
    Task<ISearchResponse<T>> SearchDocumentsAsync<T>(string query, string field) where T : class;
}
