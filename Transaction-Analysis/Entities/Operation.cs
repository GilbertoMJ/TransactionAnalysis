using Transaction_Analysis.Entities;

namespace TransactionAnalysis.Entities
{
    public class Operation
    {
        public string FavoredBank {  get; set; }
        public string FavoredBranch { get; set; }
        public string FavoredAccount { get; set; }
        public decimal Value { get; set; }
        public Account Account { get; set; }
        public Contact Contact { get; set; }
        public DateTime UTCCreationTime { get; set; }
    }
}
