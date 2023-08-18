using InMetrics.BusinessRules;
using MongoDB.Bson.Serialization.Attributes;

namespace InMetrics.Models.Entities
{
    /// <summary>
    /// Transaction class
    /// </summary>
    public class Transaction
    {
        [BsonId]
        public Guid TransactionId { get; set; }
        /// <summary>
        /// creation date
        /// </summary>
        public DateTime Creation { get; set; }
        /// <summary>
        /// UserId
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// transaction values
        /// </summary>
        [GreaterThanZeroDecimal]
        public decimal Amount { get; set; }
        /// <summary>
        /// true when is addition
        /// false when substraction
        /// </summary>
        public bool IsAddition { get; set; }
    }
}