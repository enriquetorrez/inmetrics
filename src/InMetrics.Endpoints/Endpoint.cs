namespace InMetrics.Endpoints
{
    public static class EndPoint
    {
        /// <summary>
        /// "https://transaction-api-inmetrics.azurewebsites.net/;https://localhost:7179/";
        /// </summary>
        public static string BaseUrl = "https://transaction-api-inmetrics.azurewebsites.net/";
        /// <summary>
        /// api/Transaction
        /// </summary>
        public static string Transaction = $"{BaseUrl}api/Transaction/";
        /// <summary>
        /// api/TransactionSummary/
        /// </summary>
        public static string TransactionSummary = $"{BaseUrl}api/TransactionSummary/";
    }
}