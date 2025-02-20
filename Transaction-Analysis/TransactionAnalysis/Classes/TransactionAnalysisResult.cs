using TransactionAnalysis.Domain.Enums;

namespace TransactionAnalysis.Domain.Classes
{
    public class TransactionAnalysisResult
    {
        public bool BlockedTransaction { get; private set; }
        public bool SuspectTransaction { get; private set; }
        public bool ExemptTransaction { get; private set; }

        public IList<string> BlockingReasons { get; private set; }
        public IList<string> SuspicionReasons { get; private set; }
        public IList<string> ExemptionReasons { get; private set; }

        public TransactionAnalysisResult()
        {
            BlockingReasons = new List<string>();
            SuspicionReasons = new List<string>();
            ExemptionReasons = new List<string>();
        }

        public void AddBlockingRuleAnalysis(BlockingRuleAnalysisResult result)
        {
            if (result == null)
                return;

            switch (result.Severity)
            {
                case Severity.Suspect:
                    SuspectTransaction = true;
                    SuspicionReasons.Add(result.Reason);
                    break;
                case Severity.Blocked:
                    BlockedTransaction = true;
                    BlockingReasons.Add(result.Reason);
                    break;
                default:
                    break;
            }
        }

        public void AddExemptionRuleAnalysis(ExemptionRuleAnalysisResult result)
        {
            if (result != null)
            {
                ExemptionReasons.Add(result.Reason);
                ExemptTransaction = true;
            }
        }
    }
}
