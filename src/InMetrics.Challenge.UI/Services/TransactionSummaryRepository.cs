using HttpLibrary.Services;
using InMetrics.Challenge.UI.Contracts;
using InMetrics.Models.Entities;

namespace InMetrics.Challenge.UI.Services
{
    public class TransactionSummaryRepository : BaseRepository<TransactionSummary>, ITransactionSummaryRepository
    {
        private readonly IHttpClientFactory _client;
        public TransactionSummaryRepository(IHttpClientFactory client) : base(client)
        {
            _client = client;
        }
    }
}
