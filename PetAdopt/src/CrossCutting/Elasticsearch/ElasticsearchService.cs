using Nest;

namespace CrossCutting.Elasticsearch;

public class ElasticsearchService(IElasticClient elasticClient) : IElasticsearchService
{
    private readonly IElasticClient _elasticClient = elasticClient;

    public async Task IndexDocumentAsync<T>(T document, string indexName) where T : class
    {
        IndexResponse response = await _elasticClient.IndexAsync(document, idx => idx.Index(indexName));
        if (!response.IsValid)
        {
            throw new Exception($"Error: {response.ServerError?.Error?.Reason}");
        }
    }

    public async Task<ISearchResponse<T>> SearchDocumentsAsync<T>(string query, string field) where T : class
    {
        ISearchResponse<T> response = await _elasticClient.SearchAsync<T>(s => s
            .Query(q => q
                .Match(m => m
                    .Field(field)
                    .Query(query)
                )
            )
        );

        if (!response.IsValid)
        {
            throw new Exception($"Search Error: {response.ServerError?.Error?.Reason}");
        }

        return response;
    }
}
