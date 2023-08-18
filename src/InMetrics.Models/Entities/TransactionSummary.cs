namespace InMetrics.Models.Entities
{
    /// <summary>
    /// This is a view-model class
    /// In order to represent data
    /// </summary>
    public class TransactionSummary
    {
        public DateTime Date { get; set; }
        /// <summary>
        /// Credit - Debit
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// incomming values
        /// </summary>
        public decimal Credit { get; set; }
        /// <summary>
        /// outcoming values
        /// </summary>
        public decimal Debit { get; set; }

    }
}
