using InMetrics.Models.Entities;
using InMetrics.Models.MongoDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InMetrics.TransactionManagement.API.Controllers
{
    /// <summary>
    /// Controller for generating transaction summaries.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionSummaryController : ControllerBase
    {
        /// <summary>
        /// Constructor for the TransactionSummaryController.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        /// <param name="options">MongoDB configuration options.</param>
        private readonly ILogger<TransactionController> _logger;
        private readonly MongoContext<Transaction> _transactionContext;
        public TransactionSummaryController(ILogger<TransactionController> logger, IOptions<ConfigMongoDb> options)
        {
            _logger = logger;
            ConfigMongoDb configMongoDb = new ConfigMongoDb() { MongoConnection = options.Value.MongoConnection, MongoDatabase = options.Value.MongoDatabase };
            _transactionContext = new MongoContext<Transaction>(configMongoDb);
        }
        /// <summary>
        /// Get a list of transaction summaries grouped by date.
        /// </summary>
        /// <returns>List of transaction summaries.</returns>
        // GET: api/<TransactionSummaryController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TransactionSummary>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var transactions = await _transactionContext.Do.Find(_ => true).ToListAsync();

                var groupedTransactions = transactions
                    .GroupBy(t => new { Date = t.Creation.Date })
                    .Select(group => new TransactionSummary
                    {
                        Date = group.Key.Date,
                        TotalAmount = group.Sum(t => t.IsAddition ? t.Amount : -t.Amount),
                        Credit = group.Where(t => t.IsAddition).Sum(t => t.Amount),
                        Debit = group.Where(t => !t.IsAddition).Sum(t => t.Amount)
                    })
                    .OrderBy(summary => summary.Date)
                    .ToList();

                return Ok(groupedTransactions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching transaction summaries.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
