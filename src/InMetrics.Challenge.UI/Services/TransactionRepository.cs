using HttpLibrary.Services;
using InMetrics.Challenge.UI.Contracts;
using InMetrics.Models.Entities;

namespace InMetrics.Challenge.UI.Services
{
    public class TransactionRepository : BaseRepository<Transaction>, ITransactionRepository
    {
        private readonly IHttpClientFactory _client;
        public TransactionRepository(IHttpClientFactory client) : base(client)
        {
            _client = client;
        }
    }
}
