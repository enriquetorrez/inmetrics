using InMetrics.Models.Entities;
using InMetrics.Models.MongoDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InMetrics.TransactionManagement.API.Controllers
{
    /// <summary>
    /// Controller for managing transactions.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly MongoContext<Transaction> _transactionContext;
        /// <summary>
        /// Constructor for the TransactionController.
        /// </summary>
        /// <param name="logger">Logger instance.</param>
        /// <param name="options">MongoDB configuration options.</param>
        public TransactionController(ILogger<TransactionController> logger, IOptions<ConfigMongoDb> options)
        {
            _logger = logger;
            ConfigMongoDb configMongoDb = new ConfigMongoDb() { MongoConnection = options.Value.MongoConnection, MongoDatabase = options.Value.MongoDatabase };
            _transactionContext = new MongoContext<Transaction>(configMongoDb);
        }
        /// <summary>
        /// Get a list related to all
        /// </summary>
        /// <returns>list of <see cref="Transaction"/></returns>
        // GET: api/<TransactionController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TransactionSummary>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var transactions = await _transactionContext.Do.Find(_ => true).ToListAsync();
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching transactions.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        // POST api/<TransactionController>
        /// <summary>
        /// Create a new register
        /// </summary>
        /// <param name="model"><see cref="Transaction"/></param>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TransactionSummary>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(Transaction model)
        {
            try
            {
                await _transactionContext.Do.InsertOneAsync(model);
                return CreatedAtAction(nameof(Get), new { id = model.TransactionId }, model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating transaction.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
        /// <summary>
        /// Delete a transaction by ID.
        /// </summary>
        /// <param name="id">Guid identitifer</param>
        /// <returns>No content if deleted successfully, otherwise NotFound.</returns>
        // DELETE api/<TransactionController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TransactionSummary>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var deleteResult = _transactionContext.Do.DeleteOne(_ => _.TransactionId.Equals(id));
                if (deleteResult.DeletedCount > 0)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting transaction.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
