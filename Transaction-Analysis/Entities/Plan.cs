namespace TransactionAnalysis.Entities
{
    public class Plan
    {
        public string Name { get; set; }
        public decimal TransferFee { get; set; }
        public int CurrentDayLimit { get; set; }
        public DateTime UTCCreationTime { get; set; }
    }
}
